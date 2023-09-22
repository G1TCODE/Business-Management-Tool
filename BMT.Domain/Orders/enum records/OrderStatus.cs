using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Orders
{
    public enum OrderStatus
    {
        AwaitingApproval = 0,
        Approved = 1,
        Shipped = 2,
        DeliveredToCentralWarehouse = 3,
        EnRouteToStore = 4,
        DeliveredToStore = 5,
        Cancelled = 6,
    };
}
