using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.SearchOrders
{
    public sealed record SearchOrdersQuery(Guid managerId) : IQuery<IReadOnlyList<OrderResponse>>;
}
