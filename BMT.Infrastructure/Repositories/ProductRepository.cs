using BMT.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Products;

namespace BMT.Infrastructure.Repositories
{
    internal sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
