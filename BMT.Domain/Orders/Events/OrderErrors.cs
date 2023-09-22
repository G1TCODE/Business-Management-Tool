using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;

namespace BMT.Domain.Orders.Events
{
    public static class OrderErrors
    {
        public static Error ExcessiveTotalCost = 
            new("Order.Cost", "You must be at least a Level 4 manager in order to place an order of this cost");

        public static Error OrderNotPlaced = 
            new("Order.Status","An error occurred while trying to place your order. Please try again");

        public static Error DeliveryIncident = 
            new("Order.Delivery", 
                "An incident occurred while trying to deliver your order. " +
                "You will receive additional correspondence within 24 hours.");

        public static Error NotDelivered =
            new("Order.OrderStatus", "You have attempted to mark an order as deliverd, " +
                "but the order has not yet left the central warehouse.");

        public static Error NotCancelled =
            new("Order.OrderStatus", "Your order was not able to be cancelled.");

        public static Error ManagerLevelTooLow =
            new("Order.ManagerPermissions", "You need to be a higher level manager to complete this action.");

        public static Error WrongManager =
            new("Order.ManagerPermissions", "You are not the appropriate manager to complete this task.");
    }
}
