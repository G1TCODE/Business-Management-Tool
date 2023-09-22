using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        void Add(Order order);
    }
}
