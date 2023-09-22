using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;

namespace BMT.Domain.Orders.Events
{
    public record OrderCreatedDomainEvent(Guid OrderID) : IDomainEvent;
}
