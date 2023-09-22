using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Orders;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.ShoppingCart.AddItem
{
    public record AddItemCommand
        (Guid managerid,
        Guid shoppingcartid,
        Guid productid,
        Guid storeorwarehouseid) : ICommand<Guid>;
}
