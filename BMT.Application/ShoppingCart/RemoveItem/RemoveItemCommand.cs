using BMT.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.ShoppingCart.RemoveItem
{
    public record RemoveItemCommand(Guid managerid, Guid shoppingcartid, Guid itemid)
        : ICommand<Guid>;
}
