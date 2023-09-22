using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Infrastructure.Repositories
{
    internal sealed class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
