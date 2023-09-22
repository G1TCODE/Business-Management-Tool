using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.OrderDeliveredToStore
{
    public record OrderDeliveredToStoreCommand(Guid managerid, Guid orderid, Guid storeid) : ICommand;
}
