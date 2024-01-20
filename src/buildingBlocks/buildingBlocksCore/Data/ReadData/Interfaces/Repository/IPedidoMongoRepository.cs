using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksServices.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Interfaces.Repository
{
    public interface IPedidoMongoRepository : IBaseRepositoryMongo<PedidoMongo>
    {
        Task<PedidoMongo> GetPedidoByRelationalId(string relationalId);

        Task<PedidoMongo> GetPedidoUpdateByRelationalId(string relationalId);

        Task UpdatePedidoMongo(PedidoMongo x);

        Task<PagedDataResponse<PedidoMongo>> PagedPedidos(PedidoPagedRequest clientePagedRequest);
    }
}
