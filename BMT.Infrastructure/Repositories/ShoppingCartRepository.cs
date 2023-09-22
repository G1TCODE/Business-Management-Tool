using BMT.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Shopping_Cart;

namespace BMT.Infrastructure.Repositories
{
    internal sealed class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
