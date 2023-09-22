using BMT.Application.Abstractions.Messaging;
using BMT.Application.Orders.SearchOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Manager.SearchManagers
{
    public sealed record SearchManagersQuery(Guid managerId) : IQuery<IReadOnlyList<ManagerResponse>>;
}
