using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Shopping_Cart
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(ShoppingCart shoppingCart);
    }
}
