using apiGatewaysUser.Services;
using Asp.Versioning;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;
using buildingBlocksServices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiGatewaysUser.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/apiGatewayUser")]
    [Authorize]
    public class UserController : MainController
    {
        readonly IUserService _userService;
        readonly ICustomerService _customerService;
        public UserController(ICustomerService customerService, IUserService userService, LNotifications notifications)
            : base(notifications)
        {
            _userService = userService;
            _customerService = customerService;
        }

        /**/

        [HttpPut]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Get([FromQuery] UserListRequest userListRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerAsync(() => _customerService.Get(userListRequest));
        }


        [HttpPost("nova-conta")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegister)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.UserRegisterRequestAsync(userRegister));

        }

        [HttpPut("atualizar-conta")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _customerService.UpdateCustomer(updateUserRequest));

        }

        [HttpDelete("id:guid")]
        [ClaimsAuthorize("UsersAdm", "1")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.DeleteUserAsync(id));

        }


        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _userService.LoginAsync(userLogin));
        }
    }
}
