using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.ShoppingCart.IncreaseQuantity
{
    public record IncreaseItemQuantityByCommand(Guid managerid, Guid shoppingcartid, Guid itemid, int increaseBy)
        : ICommand<Guid>;
}
