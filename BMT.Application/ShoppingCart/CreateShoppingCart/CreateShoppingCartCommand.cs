using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.ShoppingCart.CreateShoppingCart
{
    public record CreateShoppingCartCommand(Guid managerid, Guid storeorwarehouseid, OrderType ordertype)
        : ICommand<Guid>;
}
