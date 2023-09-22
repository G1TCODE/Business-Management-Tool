using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Manager.CreateManager
{
    public record CreateManagerCommand(
            Guid bossId, 
            string newHireName,
            string newHirePassword,
            string newHireEmail,
            ManagerScope newHireScope,
            Guid newHireStoreId) : ICommand<Guid>;
}
