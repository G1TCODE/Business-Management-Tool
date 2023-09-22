using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Orders;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.CancelOrder
{
    public record CancelOrderCommand(Guid managerid, Guid orderid, Guid storeorwarehouseid) : ICommand;
}
