using Asp.Versioning;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Utils;
using buildingBlocksServices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using supplierApi.Application.Commands.Supplier;

namespace supplierApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/supplier")]
    [Authorize]

    public class SupplierController : MainController
    {

        readonly IMediatorHandler _mediatorHandler;
        public SupplierController(IMediatorHandler mediatorHandler, LNotifications notifications) : base(notifications)
        {
            _mediatorHandler = mediatorHandler; 
        }

        [HttpPost]
        public async Task<IActionResult> PostSupplier([FromBody] InsertSupplierCommand  insertSupplierCommand)
        {
            var respCommand = await _mediatorHandler.SendCommand<InsertSupplierCommand, InsertSupplierResponseCommand>(insertSupplierCommand);
            if (respCommand.Notifications.Any())
                _notifications.AddRange(respCommand.Notifications);
            return Response(respCommand.Response.Id);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSupplierCommand([FromRoute] Guid id)
        {

            var respCommand = await _mediatorHandler.SendCommand<DeleteSupplierCommand, object>(new DeleteSupplierCommand { Id = id });
            if (respCommand.Notifications.Any())
                _notifications.AddRange(respCommand.Notifications);
            return Response(null);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierCommand updateSupplierCommand)
        {
            var respCommand = await _mediatorHandler.SendCommand<UpdateSupplierCommand, object>(updateSupplierCommand);
            if (respCommand.Notifications.Any())
                _notifications.AddRange(respCommand.Notifications);
            return Response(null);
        }
    }
}
