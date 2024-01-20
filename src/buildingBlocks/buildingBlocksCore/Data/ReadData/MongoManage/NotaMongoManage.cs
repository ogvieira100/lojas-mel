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
    public class NotaMongoManage : INotaMongoManage
    {


        readonly IClienteQuery _clienteQuery;
        readonly IProdutoQuery _produtoQuery;
        readonly IFornecedorQuery _fornedorQuery;
        readonly IMongoCollection<NotaMongo> _notaCollection;
        readonly INotaQuery _notaQuery;
        readonly IPedidoMongoRepository _pedidoMongoRepository;

        public NotaMongoManage(IFornecedorQuery fornedorQuery,
                                 IClienteQuery clienteQuery,
                                 INotaQuery notaQuery,
                                 MongoContext contextMongo,
                                 IPedidoMongoRepository pedidoMongoRepository,
                                 IProdutoQuery produtoQuery)
        {
            _clienteQuery = clienteQuery;
            _produtoQuery = produtoQuery;
            _fornedorQuery = fornedorQuery;
            _notaQuery = notaQuery;
            _pedidoMongoRepository = pedidoMongoRepository;
            _notaCollection = contextMongo.DB.GetCollection<NotaMongo>(new NotaMongo().TableName);
        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Nota>> notas)
        {
            foreach (var nota in notas)
            {

                switch (nota.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(nota.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(nota.Item2);
                        break;
                    default:
                        break;
                }

            }
        }

        private async Task InsertAsync(Nota item2)
        {

            var clienteMongo = await _clienteQuery.GetCliMongoByRelationId(item2.ClienteId.ToString());
            var fornMongo = await _fornedorQuery.GetFornecedorMongoByRelationId(item2.FornecedorId.ToString());
            var produtosPedido = await _produtoQuery.GetProdutosMongoByRelationsIds(item2.NotaItens.Select(x => x.ProdutoId.ToString()));



            var notaMongo = new NotaMongo();
            notaMongo.Observation = item2.Observacao ?? "";
            notaMongo.FornecedorId = item2.FornecedorId.ToString();
            notaMongo.RelationalId = item2.Id.ToString();
            notaMongo.ClienteId = item2.ClienteId.ToString();
            notaMongo.Cliente = clienteMongo;
            notaMongo.Fornecedor = fornMongo;
            notaMongo.Numero = item2.Numero;

            foreach (var pedidoItem in item2.NotaItens)
            {

                var prod = produtosPedido.FirstOrDefault(x => x.RelationalId == pedidoItem.ProdutoId.ToString());
                notaMongo.NotaItens.Add(new NotaItensMongo
                {
                    Price = pedidoItem.Price,
                    Qtd = pedidoItem.Qtd,
                    ProdutoId = prod.RelationalId,
                    RelationalId = pedidoItem.Id.ToString(),
                    Produto = prod
                });
            }

            await _notaCollection.InsertOneAsync(notaMongo);
        }

        private async Task DeleteAsync(Nota item2)
        {
            var pedidoMongo = await _notaQuery.GetNotaUpdateByRelationalId(item2.Id.ToString());
            await _notaCollection.DeleteOneAsync(x => x.RelationalId == pedidoMongo.Id.ToString());
        }
    }
}
