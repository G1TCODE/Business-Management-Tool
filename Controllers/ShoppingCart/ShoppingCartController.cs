using BMT.Application.Abstractions.Messaging;
using BMT.Application.Orders.ApproveOrder;
using BMT.Application.ShoppingCart.AddItem;
using BMT.Application.ShoppingCart.CreateShoppingCart;
using BMT.Application.ShoppingCart.IncreaseQuantity;
using BMT.Application.ShoppingCart.RemoveItem;
using BMT.Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMT.API.Controllers.ShoppingCart
{
    [ApiController]
    [Route("api/shoppingcarts")]
    public class ShoppingCartController : ControllerBase
    {
        private ISender _thisSender;

        public ShoppingCartController(ISender thisSender)
        {
            _thisSender = thisSender;
        }

        [HttpPost("/createcart")]
        public async Task<IActionResult> CreateShoppingCart(
            Guid managerId, 
            Guid storeId, 
            OrderType orderType, 
            CancellationToken cancellationToken)
        {
            var command = new CreateShoppingCartCommand(managerId, storeId, orderType);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("/additemtocart")]
        public async Task<IActionResult> AddItem(
            Guid managerId,
            Guid shoppingCartId,
            Guid productId,
            Guid storeId,
            CancellationToken cancellationToken)
        {
            var command = new AddItemCommand(managerId, shoppingCartId, productId, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("/removeitemfromcart")]
        public async Task<IActionResult> RemoveItem(
            Guid managerId,
            Guid shoppingCartId,
            Guid itemId,
            CancellationToken cancellationToken)
        {
            var command = new RemoveItemCommand(managerId, shoppingCartId, itemId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("/increaseq")]
        public async Task<IActionResult> IncreaseQuantityByItem(
            Guid managerId,
            Guid shoppingCartId,
            Guid itemId,
            int increaseBy,
            CancellationToken cancellationToken)
        {
            var command = new IncreaseItemQuantityByCommand(managerId, shoppingCartId, itemId, increaseBy);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}