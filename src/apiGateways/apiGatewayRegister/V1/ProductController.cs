using Asp.Versioning;
using buildingBlocksCore.Utils;
using buildingBlocksServices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiGatewayRegister.V1
{


    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/apiGatewayRegisterProduct")]
    [Authorize]
    public class ProductController : MainController
    {
        public ProductController(LNotifications notifications) : base(notifications)
        {

        }
    }
}
