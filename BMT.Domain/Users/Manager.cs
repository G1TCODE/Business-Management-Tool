using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;
using BMT.Domain.Shared;
using BMT.Domain.Orders;
using BMT.Domain.Locations;
using BMT.Domain.Orders.Events;
using BMT.Domain.Users.Events;
using BMT.Domain.Shopping_Cart;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace BMT.Domain.Users
{
    public class Manager : Entity
    {

        IManagerRepository _managerRepository;

        #region Constructor
        public Manager(Guid id) : base(id)

        {

        }
        public Manager
            (Guid id, 
            Name name, 
            Password password, 
            Email email,
            ManagerScope managerscope,
            Guid storeid) 
            : base(id)
        {
            Name = name;
            Password = password;
            Email = email;
            ManagerScope = managerscope;
            StoreOrWarehouseId = storeid;
        }

        #endregion Constructor

        #region Collections

        public List<Manager> StoreManagers = new List<Manager>();

        public List<Order> StoreOrders = new List<Order>();

        public List<Order> CompanyOrders = new List<Order>();

        public List<Order> CustomerOrders = new List<Order>();

        #endregion Collections

        #region Methods

        #region Factory Method

        public static Manager CreateManager(
            Guid id,
            Name name,
            Password password,
            Email email,
            ManagerScope managerscope,
            Guid storeid)
        {
            Manager newManager = new Manager
            (Guid.NewGuid(),
            name,
            password,
            email,
            managerscope,
            storeid);

            newManager.Level = Level.Level1;

            newManager.RaiseDomainEvent(new ManagerCreatedDomainEvent(newManager.Id));

            newManager.StoreManagers.Add(newManager);

            return newManager;
        }

        #endregion Factory Method
        public Result MarkAsApproved(Manager manager, Order order, Store thisStore)
        {
            if (manager.Level < Level.Level4)
            {
                return Result.ActionFailed(OrderErrors.ManagerLevelTooLow);
            }

            order.OrderStatus = OrderStatus.Approved;

            foreach (Item item in order.OrderItems)
            {
                thisStore.InventoryOnOrder.Add(item);
            }

            if (order.OrderType == OrderType.Customer)
            {
                CustomerOrders.Add(order);

                foreach (Item item in order.OrderItems)
                {
                    thisStore.InventoryOnOrder.Add(item);
                }
            }

            if (order.OrderType == OrderType.Store)
            {
                StoreOrders.Add(order);

                foreach (Item item in order.OrderItems)
                {
                    thisStore.InventoryOnOrder.Add(item);
                    //centralwarehouse.InventoryOnOrder.Add(item);
                }
            }

            if (order.OrderType == OrderType.Company)
            {
                CompanyOrders.Add(order);

                foreach (Item item in order.OrderItems)
                {
                    thisStore.InventoryOnOrder.Add(item);
                }
            }

            manager.RaiseDomainEvent(new OrderApprovedDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }

        public static Result MarkAsShipped(Manager manager, Order order, Store thisStore)
        {
            if (manager.Level < Level.Level4)
            {
                return Result.ActionFailed(OrderErrors.ManagerLevelTooLow);
            }

            order.OrderStatus = OrderStatus.Shipped;

            manager.RaiseDomainEvent(new OrderShippedDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }

        public static Result MarkAsDeliverdToCentralWarehouse(Manager manager, Order order, Store thisStore)
        {
            if (manager.Level == Level.Level1)
            {
                return Result.ActionFailed(OrderErrors.ManagerLevelTooLow);
            }

            order.OrderStatus = OrderStatus.DeliveredToCentralWarehouse;

            //foreach (Item item in order.OrderItems)
            //{
            //    centralwarehouse.InventoryOnHand.Add(item);
            //}

            manager.RaiseDomainEvent(new OrderDeliveredToCentralWarehouseDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }
        public static Result MarkAsEnRouteToStore(Manager manager, Order order, Store thisStore)
        {
            if (manager.Level < Level.Level4)
            {
                return Result.ActionFailed(OrderErrors.ManagerLevelTooLow);
            }

            order.OrderStatus = OrderStatus.EnRouteToStore;

            //foreach (Item item in order.OrderItems)
            //{
            //    centralwarehouse.InventoryOnHand.Remove(item);
            //}

            manager.RaiseDomainEvent(new OrderEnRouteToStoreDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }

        public static Result MarkAsDeliveredToStore(Manager manager, Order order, Store thisStore)
        {
            if (order.OrderStatus != OrderStatus.EnRouteToStore)
            {
                return Result.ActionFailed(OrderErrors.NotDelivered);
            }

            if (manager.Id != order.ManagerId)
            {
                return Result.ActionFailed(OrderErrors.WrongManager);
            }

            order.OrderStatus = OrderStatus.DeliveredToStore;

            foreach (Item item in order.OrderItems)
            {
                thisStore.InventoryOnOrder.Remove(item);
                thisStore.InventoryOnHand.Add(item);
            }

            manager.RaiseDomainEvent(new OrderDeliveredToStoreDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }

        public Result MarkAsCancelled(Manager manager, Order order, Store thisStore)
        {
            if (order.OrderStatus >= OrderStatus.DeliveredToCentralWarehouse)
            {
                return Result.ActionFailed(OrderErrors.NotCancelled);
            }

            if (manager.Level < Level.Level4)
            {
                return Result.ActionFailed(OrderErrors.WrongManager);
            }

            order.OrderStatus = OrderStatus.Cancelled;

            if (order.OrderType == OrderType.Customer)
            {
                CustomerOrders.Remove(order);

                foreach (Item item in order.OrderItems)
                {
                    thisStore.InventoryOnOrder.Remove(item);
                }
            }

            else if (order.OrderType == OrderType.Store)
            {
                StoreOrders.Remove(order);

                foreach (Item item in order.OrderItems)
                {
                    thisStore.InventoryOnOrder.Remove(item);
                }
            }

            manager.RaiseDomainEvent(new OrderCancelledDomainEvent(order.Id));

            return Result.ActionSuccessful();
        }

        public static Result PromoteManager(Manager boss, Manager toPromote)
        {
            if (boss.Level < Level.Level4 || boss.ManagerScope != ManagerScope.RegionalLevel)
            {
                return Result.ActionFailed(OrderErrors.WrongManager);
            }

            if (toPromote.Level == Level.Level1)
            {
                toPromote.Level = Level.Level2;
            }

            else if (toPromote.Level == Level.Level2)
            {
                toPromote.Level = Level.Level3;
            }

            else if (toPromote.Level == Level.Level3)
            {
                toPromote.Level = Level.Level4;
            }

            else if (toPromote.Level == Level.Level4)
            {
                toPromote.Level = Level.Level5;
            }

            if (toPromote.Level > Level.Level3)
            {
                toPromote.ManagerScope = ManagerScope.RegionalLevel;
            }

            toPromote.RaiseDomainEvent(new ManagerPromotedDomainEvent(toPromote.Id));

            return Result.ActionSuccessful();
        }

        #endregion Methods

        #region Properties

        public Name Name { get; init; }

        public Level Level { get; private set; }

        [Column("password")]
        public Password Password { get; private set; }

        [Column("email")]
        public Email Email { get; private set; }

        [Column("manager_scope")]
        public ManagerScope ManagerScope { get; private set; }

        [Column("store_or_warehouse_id")]
        public Guid StoreOrWarehouseId { get; private set; }

        #endregion Properties

    }
}
