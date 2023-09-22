using BMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Orders.Events
{
    public record OrderCancelledDomainEvent(Guid id) : IDomainEvent;
}
