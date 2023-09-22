using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Manager.PromoteManager
{
    public record PromoteManagerCommand(Guid bossId, Guid toPromoteId)
        : ICommand<Guid>;
}
