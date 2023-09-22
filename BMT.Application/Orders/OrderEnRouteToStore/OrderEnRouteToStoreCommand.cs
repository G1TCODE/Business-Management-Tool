using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.OrderEnRouteToStore
{
    public record OrderEnRouteToStoreCommand(Guid managerid, Guid orderid, Guid storeorwarehouseid) : ICommand;
}
