using BMT.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Locations;

namespace BMT.Infrastructure.Repositories
{
    internal sealed class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
