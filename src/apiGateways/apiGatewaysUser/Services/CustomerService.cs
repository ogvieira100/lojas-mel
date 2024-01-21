using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;
using System.Net.Http;

namespace apiGatewaysUser.Services
{

    public interface ICustomerService
    {

        Task<BaseResponseApi<UpdateUserResponse>> UpdateCustomer(UpdateUserRequest updateUserRequest);

        Task<PagedDataResponse<CustomerDto>> Get(UserListRequest userListRequest);
    }
    public class CustomerService : BaseService, ICustomerService
    {

        readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient, LNotifications notification) : base(notification)
        {
            _httpClient = httpClient;   
        }

        public async Task<PagedDataResponse<CustomerDto>> Get(UserListRequest userListRequest)
        {
            var resp = new PagedDataResponse<CustomerDto>();



            return resp;
        }

        public async Task<BaseResponseApi<UpdateUserResponse>> UpdateCustomer(UpdateUserRequest updateUserRequest)
        {

            var httpContent = GetContentJsonUTF8(updateUserRequest);
            var responseLogin = await _httpClient.PutAsync($"api/v1/customer", httpContent);
            await TreatErrorsResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin);
            if (_notification.Any())
                return null;
            return (await DeserializeObjResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin));
        }
    }
}
