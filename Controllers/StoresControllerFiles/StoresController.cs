using BMT.Application.Orders.SearchOrders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BMT.Application.Stores;

namespace BMT.API.Controllers.StoresControllerFiles
{
    [ApiController]
    [Route("api/stores")]
    public class StoresController : ControllerBase
    {
        private ISender _thisSender;

        public StoresController(ISender thisSender)
        {
            _thisSender = thisSender;
        }

        [HttpGet]
        public async Task<IActionResult> ViewStores(Guid managerId, CancellationToken cancellationToken)
        {
            var storesQuery = new ViewStoresQuery(managerId);

            var result = await _thisSender.Send(storesQuery, cancellationToken);

            return result.ResultSuccessful ? Ok(result.Value) : NotFound();
        }
    }
}
