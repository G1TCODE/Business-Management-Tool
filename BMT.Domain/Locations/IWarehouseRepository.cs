using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Locations
{
    public interface ICentralWarehouseRepository
    {
        Task<CentralWarehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(CentralWarehouse centralWarehouse);
    }
}
