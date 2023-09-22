using BMT.Application.Abstractions.Messaging;
using BMT.Application.Manager.SearchManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Stores
{
    public sealed record ViewStoresQuery(Guid managerId) : IQuery<IReadOnlyList<StoreResponse>>;
}
