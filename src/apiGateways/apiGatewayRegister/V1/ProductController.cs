using apiGatewayRegister.Services;
using Asp.Versioning;
using buildingBlocksCore.Models.Request;
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

        readonly IProductService _productService;
        public ProductController(IProductService productService,LNotifications notifications) : base(notifications)
        {
            _productService = productService;   
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ProductRegisterRequest productRegisterRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _productService.ProductRegisterAsync(productRegisterRequest));
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest  productUpdateRequest)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _productService.ProductUpdateAsync(productUpdateRequest));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return ReturnModelState(ModelState);
            return await ExecControllerApiGatewayAsync(() => _productService.ProductDeleteAsync(id));

        }
    }
}
