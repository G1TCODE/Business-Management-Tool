using BMT.Application.Abstractions.Messaging;
using BMT.Application.Manager.SearchManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Products;

namespace BMT.Application.Products
{
    public sealed record ViewProductsQuery(Guid managerId) : IQuery<IReadOnlyList<ProductResponse>>;
}
