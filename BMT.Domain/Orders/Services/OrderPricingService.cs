using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Orders;

namespace BMT.Domain.Orders.Services
{
    public class OrderPricingService
    {
        Money orderPrice;

        public Money CalculateOrderPrice(List<Item> orderitems)
        {
            if (orderitems.Count == 0)
            {
                return new Money(0);
            }
            else
            {
                foreach (Item item in orderitems)
                {
                    if (item.Quantity > 1)
                    {
                        orderPrice += item.Quantity * item.Price;
                    }
                    else
                    {
                        orderPrice = item.Price;
                    }
                }
                return orderPrice;
            }
        }
    }
}
