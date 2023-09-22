using Microsoft.EntityFrameworkCore;
using BMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Infrastructure.Repositories
{
    internal abstract class Repository<T>
        where T : Entity
    
    {
        protected readonly ApplicationDbContext DBContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DBContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DBContext
                .Set<T>()
                .FirstOrDefaultAsync(manager => manager.Id == id, cancellationToken);
        }

        public void Add(T entity)
        {
            DBContext.Add(entity);
        }
    }
}
