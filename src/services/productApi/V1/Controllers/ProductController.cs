using Asp.Versioning;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Utils;
using buildingBlocksServices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using productApi.Application.Commands.Products;

namespace productApi.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/product")]
    [Authorize]

    public class ProductController : MainController
    {
        readonly IMediatorHandler _mediatorHandler;
        public ProductController(IMediatorHandler mediatorHandler, LNotifications notifications) : base(notifications)
        {
            _mediatorHandler = mediatorHandler;
        }


        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] string filtro)
        {

            return Response(null);
        
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct([FromQuery] string filtro)
        {

            return Response(null);

        }



        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] string filtro)
        {

            return Response(null);

        }



        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var respCommand = await _mediatorHandler.SendCommand<UpdateProductCommand, object>(updateProductCommand);
            if (respCommand.Notifications.Any())
                _notifications.AddRange(respCommand.Notifications);
            return Response(null);
        }
    }
}
