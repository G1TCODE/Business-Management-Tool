using BMT.Domain.Orders;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.SearchOrders
{
    public sealed class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid StoreOrWarehouseId { get; set; }
        public Guid ManagerId { get; init; }
        public int OrderStatus { get; internal init; }
        public DateTime DateOrderPlaced { get; set; }
        public int TotalCost { get; init; }
        public int OrderType { get; set; }
    }
}
