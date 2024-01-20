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
    public class FornecedorMongoManage : IFornecedorMongoManage
    {
        readonly IMongoCollection<FornecedorMongo> _fornecedorCollection;

        public FornecedorMongoManage(MongoContext contextMongo)
        {
            _fornecedorCollection = contextMongo.DB.GetCollection<FornecedorMongo>(new FornecedorMongo().TableName);

        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Fornecedor>> fornecedores)
        {

            foreach (var produto in fornecedores)
            {

                switch (produto.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(produto.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        await UpdateAsync(produto.Item2);
                        break;
                    case    Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(produto.Item2);
                        break;
                    default:
                        break;
                }

            }
        }

        private async Task DeleteAsync(Fornecedor forn)
        {
            if (forn != null)
            {
                var fornMongoDelete = (await _fornecedorCollection.FindAsync(x => x.RelationalId == forn.Id.ToString()))?.FirstOrDefault();
                if (fornMongoDelete != null)
                    await _fornecedorCollection.DeleteOneAsync(x => x.RelationalId == forn.Id.ToString());
            }
        }

        async Task UpdateAsync(Fornecedor fornecedor)
        {
            var forn = fornecedor;
            var fornecedorMongo = (await _fornecedorCollection.FindAsync(x => x.RelationalId == forn.Id.ToString()))?.FirstOrDefault();
            if (fornecedorMongo is not null
                && forn is not null)
            {
                fornecedorMongo.CNPJ = forn.CNPJ;
                fornecedorMongo.RazaoSocial = forn.RazaoSocial;
                fornecedorMongo.RelationalId = forn.Id.ToString();


                await _fornecedorCollection.DeleteOneAsync(x => x.Id == fornecedorMongo.Id);
                await _fornecedorCollection.InsertOneAsync(fornecedorMongo);

                //var cliMongoId = cliMongo.Id;
                //var atualizacao = Builders<ClientesMongo>.Update.Set(_ => _, cliMongo);
                //await _clientesCollection.UpdateOneAsync(_ => _.Id == cliMongoId, atualizacao);
            }
        }

        async Task InsertAsync(Fornecedor fornecedor)
        {
            var fornecedorMongo = new FornecedorMongo();
            fornecedorMongo.CNPJ = fornecedor.CNPJ;
            fornecedorMongo.RazaoSocial = fornecedor.RazaoSocial;
            fornecedorMongo.RelationalId = fornecedor.Id.ToString() ?? "";
            await _fornecedorCollection.InsertOneAsync(fornecedorMongo);
        }
    }
}
