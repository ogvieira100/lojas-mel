using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Data.ReadData.Repository;
using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Query
{
    public class ClienteQuery : IClienteQuery
    {

        readonly IClienteMongoRepository _clienteMongoRepository;

        public ClienteQuery(IClienteMongoRepository clienteMongoRepository)
        {
            _clienteMongoRepository = clienteMongoRepository;
        }

        public async Task<ClientesMongo> GetCliMongoByRelationId(string relationalId)
        => await _clienteMongoRepository.RepositoryConsultMongo.GetByIdAsync(relationalId);

        public async Task<PagedDataResponse<ClientesMongo>> PagedCliente(ClientePagedRequest clientePagedRequest)
        {
            return await _clienteMongoRepository.RepositoryConsultMongo.PaginateAsync(clientePagedRequest, null);
        }
    }
}
