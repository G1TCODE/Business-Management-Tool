using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Orders.ApproveOrder
{
    public record ApproveOrderCommand(Guid managerid, Guid orderid, Guid storeid) : ICommand;
}
