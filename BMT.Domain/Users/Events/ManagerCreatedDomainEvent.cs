using BMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Users.Events
{
    public record ManagerCreatedDomainEvent(Guid ManagerID) : IDomainEvent;
}
