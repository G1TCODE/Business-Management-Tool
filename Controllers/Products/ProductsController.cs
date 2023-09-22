using BMT.Application.Orders.SearchOrders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BMT.Application.Products;

namespace BMT.API.Controllers.Products
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private ISender _thisSender;
        IMediator _mediator;
        public ProductsController(ISender thisSender, IMediator mediator)
        {
            _thisSender = thisSender;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProducts(Guid managerId, CancellationToken cancellationToken)
        {
            var productsQuery = new ViewProductsQuery(managerId);

            var result = await _thisSender.Send(productsQuery, cancellationToken);

            return result.ResultSuccessful ? Ok(result.Value) : NotFound();
        }
    }
}
