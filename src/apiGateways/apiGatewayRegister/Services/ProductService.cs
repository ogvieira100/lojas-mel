using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;
using System.Net.Http;

namespace apiGatewayRegister.Services
{

    public interface IProductService
    {
        Task<BaseResponseApi<ProductRegisterResponse>> ProductRegisterAsync(ProductRegisterRequest userRegisterRequest);

        Task<BaseResponseApi<object>> ProductUpdateAsync(ProductUpdateRequest userLoginRequest);

        Task<BaseResponseApi<object>> ProductDeleteAsync(Guid id);
    }
    public class ProductService : BaseService, IProductService
    {
        readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient, LNotifications notification) : base(notification)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseApi<object>> ProductDeleteAsync(Guid id)
        {
            var responseLogin = await _httpClient.DeleteAsync($"api/v1/user/deletar-conta/{id}");
            await TreatErrorsResponse<object>(responseLogin);

            return new BaseResponseApi<object>();
        }

        public async Task<BaseResponseApi<ProductRegisterResponse>> ProductRegisterAsync(ProductRegisterRequest  productRegisterRequest)
        {
            var httpContent = GetContentJsonUTF8(productRegisterRequest);
            var responseLogin = await _httpClient.PostAsync($"api/v1/user/nova-conta", httpContent);
            await TreatErrorsResponse<ProductRegisterResponse>(responseLogin);
            if (_notification.Any())
                return null;
            return (await DeserializeObjResponse<BaseResponseApi<ProductRegisterResponse>>(responseLogin));
        }

        public async Task<BaseResponseApi<object>> ProductUpdateAsync(ProductUpdateRequest productUpdateRequest)
        {
            var httpContent = GetContentJsonUTF8(productUpdateRequest);
            var responseLogin = await _httpClient.PostAsync($"api/v1/user/nova-conta", httpContent);
            await TreatErrorsResponse<object>(responseLogin);
            if (_notification.Any())
                return null;
            return (await DeserializeObjResponse<BaseResponseApi<object>>(responseLogin));
        }
    }
}
