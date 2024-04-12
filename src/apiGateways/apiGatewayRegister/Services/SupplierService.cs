using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;

namespace apiGatewayRegister.Services
{
    public interface ISupplierService
    {

        Task<BaseResponseApi<SupplierRegisterResponse>> SupplierRegisterAsync(SupplierRegisterRequest userRegisterRequest);

        Task<BaseResponseApi<object>> SupplierUpdateAsync(SupplierUpdateRequest userLoginRequest);

        Task<BaseResponseApi<object>> SupplierDeleteAsync(Guid id);
    }
    public class SupplierService : BaseService, ISupplierService
    {
        readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient, LNotifications notification) : base(notification)
        {
            _httpClient = httpClient;
        }

        public Task<BaseResponseApi<object>> SupplierDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseApi<SupplierRegisterResponse>> SupplierRegisterAsync(SupplierRegisterRequest userRegisterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseApi<object>> SupplierUpdateAsync(SupplierUpdateRequest userLoginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
