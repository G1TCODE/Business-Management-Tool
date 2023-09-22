using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Orders;

namespace BMT.Infrastructure.Repositories
{
    internal sealed class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
