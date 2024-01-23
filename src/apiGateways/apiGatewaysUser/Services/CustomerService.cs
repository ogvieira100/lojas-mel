using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Dto;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        readonly IClienteMongoRepository _clienteMongoRepository;
        public CustomerService(IClienteMongoRepository clienteMongoRepository, HttpClient httpClient, LNotifications notification) : base(notification)
        {
            _httpClient = httpClient;
            _clienteMongoRepository = clienteMongoRepository;
        }

        public async Task<PagedDataResponse<CustomerDto>> Get(UserListRequest userListRequest)
        {
            var resp = new PagedDataResponse<CustomerDto>();
            Expression<Func<ClientesMongo, bool>> condition = x => true;
            PagedDataResponse<ClientesMongo> clientesMongoPaged = new PagedDataResponse<ClientesMongo>();
            var consultExpression = false;

            if (!string.IsNullOrEmpty(userListRequest.Nome))
            {
                condition = condition.AndAlso(x => x.Nome == userListRequest.Nome);
                consultExpression = true;
            }

            if (consultExpression)
            {
                clientesMongoPaged = await _clienteMongoRepository.RepositoryConsultMongo.PaginateAsync(userListRequest, x=> x.Nome.Contains(userListRequest.Nome));
            }
            else
                clientesMongoPaged = await _clienteMongoRepository.RepositoryConsultMongo.PaginateAsync(userListRequest, null);

            resp.TotalItens = clientesMongoPaged.TotalItens;
            resp.Items.AddRange(clientesMongoPaged.Items.Select(x =>
                new CustomerDto
                {
                    CPF = x.CPF.FormatCPF(),
                    Nome = x.Nome,
                    Id = new Guid(x.RelationalId)
                }));
            resp.Page = clientesMongoPaged.Page;
            resp.TotalPages = clientesMongoPaged.TotalPages;    
            resp.TotalItens = clientesMongoPaged.TotalItens;
            resp.PageSize = clientesMongoPaged.PageSize;
            return resp;
        }

        public async Task<BaseResponseApi<UpdateUserResponse>> UpdateCustomer(UpdateUserRequest updateUserRequest)
        {

            var httpContent = GetContentJsonUTF8(updateUserRequest);
            var responseLogin = await _httpClient.PutAsync($"api/v1/customer", httpContent);
            await TreatErrorsResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin);
            if (_notification.Any())
                return null;
            return await DeserializeObjResponse<BaseResponseApi<UpdateUserResponse>>(responseLogin);
        }
    }
}
