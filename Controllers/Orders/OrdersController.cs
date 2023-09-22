using BMT.Application.Manager.SearchManagers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BMT.Application.Orders.SearchOrders;
using BMT.Application.Manager.PromoteManager;
using BMT.Application.Orders.ApproveOrder;

namespace BMT.API.Controllers.Orders
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private ISender _thisSender;
        IMediator _mediator;
        public OrdersController(ISender thisSender, IMediator mediator)
        {
            _thisSender = thisSender;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchOrders(Guid managerId, CancellationToken cancellationToken)
        {
            var ordersQuery = new SearchOrdersQuery(managerId);

            var result = await _thisSender.Send(ordersQuery, cancellationToken);

            return result.ResultSuccessful ? Ok(result.Value) : NotFound();
        }
    }
}
