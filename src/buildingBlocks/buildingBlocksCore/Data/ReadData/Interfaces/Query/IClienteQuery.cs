using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Interfaces.Query
{
    public interface IClienteQuery
    {
        Task<ClientesMongo> GetCliMongoByRelationId(string relationalId);

        Task<PagedDataResponse<ClientesMongo>> PagedCliente(ClientePagedRequest clientePagedRequest);
    }
}
