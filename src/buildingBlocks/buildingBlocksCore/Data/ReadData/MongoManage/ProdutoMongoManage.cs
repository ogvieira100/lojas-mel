using buildingBlocksCore.Data.ReadData.Context;
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
    public class ProdutoMongoManage : IProdutoMongoManage
    {
        readonly IMongoCollection<ProdutoMongo> _produtoCollection;

        public ProdutoMongoManage(MongoContext contextMongo)
        {
            _produtoCollection = contextMongo.DB.GetCollection<ProdutoMongo>(new ProdutoMongo().TableName);

        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Produto>> produtos)
        {

            foreach (var produto in produtos)
            {

                switch (produto.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(produto.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        await UpdateAsync(produto.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(produto.Item2);
                        break;
                    default:
                        break;
                }

            }
        }

        private async Task DeleteAsync(Produto cli)
        {
            if (cli != null)
            {
                var cliMongoDelete = (await _produtoCollection.FindAsync(x => x.RelationalId == cli.Id.ToString()))?.FirstOrDefault();
                if (cliMongoDelete != null)
                    await _produtoCollection.DeleteOneAsync(x => x.RelationalId == cli.Id.ToString());


            }
        }

        async Task UpdateAsync(Produto produto)
        {
            var prod = produto;
            var produtoMongo = (await _produtoCollection.FindAsync(x => x.RelationalId == prod.Id.ToString()))?.FirstOrDefault();
            if (produtoMongo is not null
                && prod is not null)
            {
                produtoMongo.Descricao = prod.Descricao;
                produtoMongo.RelationalId = prod.Id.ToString();


                await _produtoCollection.DeleteOneAsync(x => x.Id == produtoMongo.Id);
                await _produtoCollection.InsertOneAsync(produtoMongo);

                //var cliMongoId = cliMongo.Id;
                //var atualizacao = Builders<ClientesMongo>.Update.Set(_ => _, cliMongo);
                //await _clientesCollection.UpdateOneAsync(_ => _.Id == cliMongoId, atualizacao);
            }
        }

        async Task InsertAsync(Produto produto)
        {
            var produtoMongo = new ProdutoMongo();
            produtoMongo.Descricao = produto.Descricao;
            produtoMongo.RelationalId = produto.Id.ToString() ?? "";
            await _produtoCollection.InsertOneAsync(produtoMongo);
        }
    }
}
