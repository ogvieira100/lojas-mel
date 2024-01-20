using Asp.Versioning;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Utils;
using buildingBlocksServices.Controllers;
using customerApi.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace customerApi.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customer")]
    [Authorize]

    public class CustomerController : MainController
    {
        readonly IMediatorHandler _mediatorHandler;
        public CustomerController(IMediatorHandler mediatorHandler, LNotifications notifications) : base(notifications)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            var respCommand = await _mediatorHandler.SendCommand<UpdateCustomerCommand, object>(updateCustomerCommand);
            if (respCommand.Notifications.Any())
                _notifications.AddRange(respCommand.Notifications);
            return Response(null);
        }
    }
}
