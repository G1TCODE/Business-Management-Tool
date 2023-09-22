using Azure.Identity;
using BMT.Application.Abstractions.Messaging;
using BMT.Application.Manager.CreateManager;
using BMT.Application.Manager.PromoteManager;
using BMT.Application.Manager.SearchManagers;
using BMT.Application.Orders.ApproveOrder;
using BMT.Application.Orders.CancelOrder;
using BMT.Application.Orders.OrderDeliveredToCentralWarehouse;
using BMT.Application.Orders.OrderDeliveredToStore;
using BMT.Application.Orders.OrderEnRouteToStore;
using BMT.Application.Orders.OrderShipped;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders;
using BMT.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BMT.Application.Orders.PlaceOrder;

namespace BMT.API.Controllers.Managers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagersController : ControllerBase
    {

        private ISender _thisSender;

        public ManagersController(ISender thisSender) 
        {
            _thisSender = thisSender;
        }

        #region Search Orders Get

        [HttpGet("/searchmanagers")]
        public async Task<IActionResult> SearchManagers(Guid managerId, CancellationToken cancellationToken)
        {
            var managersQuery = new SearchManagersQuery(managerId);

            var result = await _thisSender.Send(managersQuery, cancellationToken);

            return Ok(result.Value);
        }

        #endregion Search Orders Get

        #region Place Order Post

        [HttpPost("/placeorder")]
        public async Task<IActionResult> PlaceOrder(
            Guid managerId, 
            Guid shoppingCartId, 
            OrderType orderType, 
            Money totalCost,
            Guid storeId, 
            CancellationToken cancellationToken)
        {
            var command = new PlaceOrderCommand(managerId, shoppingCartId, orderType, totalCost, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        #endregion Place Order Post

        #region People Management Posts

        [HttpPost("/promotemanager")]
        public async Task<IActionResult> PromoteManager(
            Guid bossId, Guid toPromoteId, CancellationToken cancellationToken)
        {
            var command = new PromoteManagerCommand(bossId, toPromoteId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("/hiremanager")]
        public async Task<IActionResult> HireManager(
            HireManagerRequest hireManager, CancellationToken cancellationToken)
        {
            var command = new CreateManagerCommand(
                hireManager.bossId,
                hireManager.newHireName,
                hireManager.newHirePassword,
                hireManager.newHireEmail,
                hireManager.newHireScope,
                hireManager.newHireStoreId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(SearchManagers), new { id = result.Value }, result.Value);
        }

        #endregion People Management Posts

        #region Order Status Posts

        [HttpPost("/approveorder")]
        public async Task<IActionResult> ApproveOrder(Guid managerId, Guid orderId, Guid storeId, CancellationToken cancellationToken)
        {
            var command = new ApproveOrderCommand(managerId, orderId, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("/shiporder")]
        public async Task<IActionResult> MarkOrderAsShipped(Guid managerId, Guid orderId, Guid storeId, CancellationToken cancellationToken)
        {
            var command = new OrderShippedCommand(managerId, orderId, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("/orderdeliveredtowarehouse")]
        public async Task<IActionResult> MarkOrderAsDeliveredToWarehouse(Guid managerId, Guid orderId, Guid storeorwarehouseId, CancellationToken cancellationToken)
        {
            var command = new OrderArrivedAtWarehouseCommand(managerId, orderId, storeorwarehouseId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("/orderenroutetostore")]
        public async Task<IActionResult> MarkOrderAsEnRouteToStore(Guid managerId, Guid orderId, Guid storeId, CancellationToken cancellationToken)
        {
            var command = new OrderEnRouteToStoreCommand(managerId, orderId, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("/deliveredtostore")]
        public async Task<IActionResult> MarkOrderAsDeliveredToStore(Guid managerId, Guid orderId, Guid storeId, CancellationToken cancellationToken)
        {
            var command = new OrderDeliveredToStoreCommand(managerId, orderId, storeId);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("/cancelorder")]
        public async Task<IActionResult> CancelOrder(
            Guid managerId, 
            Guid orderId, 
            Guid storeorwarehouseid, 
            CancellationToken cancellationToken)
        {
            var command = new CancelOrderCommand(managerId, orderId, storeorwarehouseid);

            var result = await _thisSender.Send(command, cancellationToken);

            if (result.ResultFailed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        #endregion Order Status Posts

    }
}