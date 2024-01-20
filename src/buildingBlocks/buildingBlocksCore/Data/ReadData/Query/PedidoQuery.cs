using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksServices.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Query
{
    public class PedidoQuery : IPedidoQuery
    {
        readonly IPedidoMongoRepository _pedidoMongoRepository;

        public PedidoQuery(IPedidoMongoRepository pedidoMongoRepository)
        {

            _pedidoMongoRepository = pedidoMongoRepository;
        }
        public async Task<PedidoMongo> GetPedidoByRelationalId(string relationalId)
        => await _pedidoMongoRepository.GetPedidoByRelationalId(relationalId);

        public async Task<PedidoMongo> GetPedidoUpdateByRelationalId(string relationalId)
        => await _pedidoMongoRepository.GetPedidoUpdateByRelationalId(relationalId);

        public async Task<PagedDataResponse<PedidoMongo>> PagedPedidos(PedidoPagedRequest clientePagedRequest)
         => await _pedidoMongoRepository.PagedPedidos(clientePagedRequest);

    }
}
