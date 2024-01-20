using buildingBlocksCore.Data.ReadData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Data.ReadData.MongoManage.Interfaces;
using buildingBlocksCore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.MongoManage
{
    public class PedidoItensMongoManage : IPedidoItensMongoManage
    {


        readonly IClienteQuery _clienteQuery;
        readonly IProdutoQuery _produtoQuery;
        readonly IFornecedorQuery _fornedorQuery;
        readonly IMongoCollection<PedidoMongo> _pedidoCollection;
        readonly IMongoCollection<PedidoItensMongo> _pedidoItemCollection;
        readonly IPedidoQuery _pedidoQuery;
        readonly IPedidoMongoRepository _pedidoMongoRepository;

        public PedidoItensMongoManage(IFornecedorQuery fornedorQuery,
                                 IClienteQuery clienteQuery,
                                 IPedidoQuery pedidoQuery,
                                 MongoContext contextMongo,
                                 IPedidoMongoRepository pedidoMongoRepository,
                                 IProdutoQuery produtoQuery)
        {
            _clienteQuery = clienteQuery;
            _produtoQuery = produtoQuery;
            _fornedorQuery = fornedorQuery;
            _pedidoQuery = pedidoQuery;
            _pedidoMongoRepository = pedidoMongoRepository;
            _pedidoCollection = contextMongo.DB.GetCollection<PedidoMongo>(new PedidoMongo().TableName);
            _pedidoItemCollection = contextMongo.DB.GetCollection<PedidoItensMongo>(new PedidoItensMongo().TableName);
        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, PedidoItens>> pedidos)
        {
            foreach (var pedido in pedidos)
            {

                switch (pedido.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(pedido.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        await UpdateAsync(pedido.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(pedido.Item2);
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task InsertAsync(PedidoItens item2)
        {


            var produtosPedido = await _produtoQuery.GetProdutoMongoByRelationId(item2.ProdutoId.ToString());
            var pedidoMongo = await _pedidoQuery.GetPedidoUpdateByRelationalId(item2.PedidoId.ToString());


            var pedidoMongoItem = new PedidoItensMongo();

            pedidoMongoItem.Qtd = item2.Qtd;
            pedidoMongoItem.RelationalId = item2.Id.ToString();
            pedidoMongoItem.Price = item2.Price;
            pedidoMongoItem.PedidoId = pedidoMongo.RelationalId.ToString();
            pedidoMongoItem.Pedido = pedidoMongo;
            pedidoMongoItem.ProdutoId = produtosPedido.RelationalId.ToString();
            pedidoMongoItem.Produto = produtosPedido;


            await _pedidoItemCollection.InsertOneAsync(pedidoMongoItem);
        }

        private async Task UpdateAsync(PedidoItens item2)
        {
            await DeleteAsync(item2);
            await InsertAsync(item2);

        }

        private async Task DeleteAsync(PedidoItens item2)
        {
            var pedidoItem = (await _pedidoItemCollection.FindAsync(x => x.RelationalId == item2.Id.ToString())).FirstOrDefault();
            await _pedidoItemCollection.DeleteOneAsync(x => x.RelationalId == pedidoItem.Id.ToString());
        }
    }
}
