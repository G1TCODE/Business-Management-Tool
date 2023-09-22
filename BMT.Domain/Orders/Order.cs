using BMT.Domain.Abstractions;
using BMT.Domain.Products;
using BMT.Domain.Shared;
using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Locations;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders.Services;
using BMT.Domain.Shopping_Cart;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMT.Domain.Orders
{
    public class Order : Entity
    {
        public Order(Guid id) : base(id)
        {

        }
        #region Constructor

        private Order(
            Guid id, 
            Guid id1, 
            Guid id2, 
            OrderStatus orderstatus, 
            DateTime dateorderplaced,
            Money totalcost,
            OrderType ordertype, 
            List<Item> orderitems) : base(id)
        {
            Guid.NewGuid();
            StoreOrWarehouseId = id1;
            ManagerId = id2;
            OrderStatus = orderstatus;
            DateOrderPlaced = dateorderplaced;
            TotalCost = totalcost;
            OrderType = ordertype;
            OrderItems = orderitems;
        }

        #endregion Constructor

        #region Properties

        [Column("store_or_warehouse_id")]
        public Guid StoreOrWarehouseId { get; init; }

        [Column("manager_id")]
        public Guid ManagerId { get; init; }

        [Column("order_status")]
        public OrderStatus OrderStatus { get; internal set; }

        [Column("date_order_placed")]
        public DateTime DateOrderPlaced { get; init; }

        [Column("total_cost")]
        public Money TotalCost { get;  set; }

        [Column("order_type")]
        public OrderType OrderType { get; init; }


        #endregion Properties

        #region Collection

        public List<Item> OrderItems = new List<Item>();

        #endregion Collection

        #region Methods

        #region Factory Method

        public static Order CreateOrder(ShoppingCart thisShoppingCart)
        {
            var newOrder = new Order(
                Guid.NewGuid(),
                thisShoppingCart.StoreOrWarehouseId,
                thisShoppingCart.ManagerId,
                OrderStatus.AwaitingApproval,
                DateTime.Now,
                thisShoppingCart.ShoppingCartTotal,
                thisShoppingCart.OrderType,
                thisShoppingCart.TheShoppingCart);

            newOrder.RaiseDomainEvent(new OrderCreatedDomainEvent(newOrder.Id));

            return newOrder;
        }

        #endregion Factory Method

        //public Result Cancel()
        //{
        //    if (OrderStatus >= OrderStatus.Shipped)
        //    {
        //        return Result.ActionFailed(OrderErrors.NotCancelled);
        //    }

        //    OrderStatus = OrderStatus.Cancelled;

        //    RaiseDomainEvent(new OrderCancelledDomainEvent(Id));

        //    return Result.ActionSuccessful();
        //}

        #endregion Methods

    }

    public class Item
    {

        #region Constructor

        internal Item()
        {

        }
        internal Item(Money itemPrice, Name itemName)
        {
            ItemID = Guid.NewGuid();
            Price = itemPrice;
            ItemName = itemName;
            Quantity = 1;
        }

        #endregion Constructor

        #region Properties

        public Guid ItemID { get; set; }
        public Money Price { get; set; }

        public Name ItemName { get; init; }

        public int Quantity { get; internal set; }

        public Product Product { get; init; }

        #endregion Properties

    }
}
