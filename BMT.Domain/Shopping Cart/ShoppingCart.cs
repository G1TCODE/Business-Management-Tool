using BMT.Domain.Abstractions;
using BMT.Domain.Orders;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders.Services;
using BMT.Domain.Products;
using BMT.Domain.Shared;
using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BMT.Domain.Orders.Events;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMT.Domain.Shopping_Cart
{
    public class ShoppingCart : Entity
    {
        #region Constructor

        public ShoppingCart(Guid id) : base(id)
        {

        }
        private ShoppingCart(
            Guid id,
            Guid storeorwarehouseid,
            Guid managerid,
            OrderType ordertype) : base(id)
        {
            StoreOrWarehouseId = storeorwarehouseid;
            ManagerId = managerid;
            OrderType = ordertype;
            ShoppingCartMaxValue = new Money(0);
            ShoppingCartMaxValue = new Money(1000);
        }

        #endregion Constructor

        #region Static Factory Method

        public static ShoppingCart CreateCart(
            Guid storeorwarehouseid,
            Guid managerid,
            OrderType ordertype)
        {
            var newCart = new ShoppingCart(
                Guid.NewGuid(),
                storeorwarehouseid,
                managerid,
                ordertype);

            return newCart;
        }

        #endregion Static Factory Method

        #region Collection

        public List<Item> TheShoppingCart = new List<Item>();

        #endregion Collection

        #region Properties

        public Guid StoreOrWarehouseId { get; private set; }

        public Guid ManagerId { get; private set; }

        public OrderType OrderType { get; private set; }

        public Money? ShoppingCartTotal { get; set; }

        public Money ShoppingCartMaxValue { get; private set; }

        #endregion Properties

        #region Methods

        public static Result AddItem(ShoppingCart thisCart, Product product, Manager manager)
        {

            if (thisCart.ShoppingCartTotal + product.UnitPrice > thisCart.ShoppingCartMaxValue
                && manager.Level < Level.Level4)
            {
                return Result.ActionFailed(OrderErrors.WrongManager);
            }

            Item newItem = new Item(product.UnitPrice, product.Name)
            {
                ItemID = Guid.NewGuid(),
                Price = product.UnitPrice,
                ItemName = product.Name,
                Quantity = 1
            };

            thisCart.TheShoppingCart.Add(newItem);

            //thisCart.ShoppingCartTotal =
            //    pricingService.CalculateOrderPrice(thisCart.TheShoppingCart);

            return Result.ActionSuccessful();
        }

        public static Result RemoveItem(ShoppingCart thisCart, Guid itemid)
        {
            foreach (Item item in thisCart.TheShoppingCart)
            {
                if (itemid == item.ItemID)
                {
                    thisCart.TheShoppingCart.Remove(item);
                }
                else
                {
                    return Result.ActionFailed(Error.Null);
                }
            }
            return Result.ActionSuccessful();
        }

        public static Result IncreaseItemQuantity(ShoppingCart thisCart, Manager manager, Guid itemid, int increaseBy)
        {
            Item thisItem;

            foreach (Item item1 in thisCart.TheShoppingCart)
            {
                if (itemid == item1.ItemID)
                {
                    thisItem = item1;

                    if ((thisCart.ShoppingCartTotal) + (increaseBy * thisItem.Price) > thisCart.ShoppingCartMaxValue
                        && manager.Level < Level.Level4)
                    {
                        return Result.ActionFailed(OrderErrors.WrongManager);
                    }

                    thisItem.Quantity += increaseBy;
                }
                else
                {
                    return Result.ActionFailed(Error.Null);
                }
            }

            return Result.ActionSuccessful();
        }

        #endregion Methods

    }

}
