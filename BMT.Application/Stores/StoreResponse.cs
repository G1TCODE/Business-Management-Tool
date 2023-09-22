using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Orders;
using BMT.Application.Orders.SearchOrders;

namespace BMT.Application.Stores
{
    public sealed class StoreResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid ManagerID { get; init; }
        public string OpeningDate { get; init; }
        public StoreAddressResponse Address { get; set; }
    }
}



