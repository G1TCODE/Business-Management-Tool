using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Users
{
    public interface IManagerRepository
    {
        Task<Manager?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Manager manager);
    }
}
