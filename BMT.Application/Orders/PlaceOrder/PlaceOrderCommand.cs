using BMT.Domain.Orders;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Shopping_Cart;

namespace BMT.Application.Orders.PlaceOrder
{
    public record PlaceOrderCommand
        (Guid managerid,
        Guid shoppingcartid,
        OrderType ordertype,
        Money totalcost, 
        Guid storeorwarehouseid) : ICommand<Guid>;
}
