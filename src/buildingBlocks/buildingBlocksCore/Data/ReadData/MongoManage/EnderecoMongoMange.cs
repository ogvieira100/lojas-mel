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
    public class EnderecoMongoMange : IEnderecoMongoMange
    {
        readonly IMongoCollection<EnderecoMongo> _enderecoCollection;


        public EnderecoMongoMange(MongoContext contextMongo)
        {
            _enderecoCollection = contextMongo.DB.GetCollection<EnderecoMongo>(new EnderecoMongo().TableName);
        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Endereco>> enderecos)
        {
            foreach (var item in enderecos)
            {

                switch (item.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(item.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        await UpdateAsync(item.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(item.Item2);
                        break;
                }
            }
        }

        private async Task DeleteAsync(Endereco item)
        {
            var end = item;
            if (end != null)
            {
                var enderecoMongoDelete = (await _enderecoCollection.FindAsync(x => x.RelationalId == end.Id.ToString()))?.FirstOrDefault();
                if (enderecoMongoDelete != null)
                {


                    await _enderecoCollection.DeleteOneAsync(x => x.RelationalId == enderecoMongoDelete.Id.ToString());
                }
            }
        }

        async Task InsertAsync(Endereco item)
        {
            var enderecoMongo = new EnderecoMongo();
            enderecoMongo.Estado = item.Estado;
            enderecoMongo.Logradouro = item.Logradouro;
            enderecoMongo.RelationalId = item.Id.ToString();
            enderecoMongo.ClienteId = item.Cliente.Id.ToString();
            if (item.Cliente is not null)
                enderecoMongo.Cliente = new ClientesMongo
                {
                    CPF = item.Cliente.CPF,
                    RelationalId = item.Id.ToString(),
                    Nome = item.Cliente.Nome
                };

            await _enderecoCollection.InsertOneAsync(enderecoMongo);
        }

        async Task UpdateAsync(Endereco item)
        {
            var end = item;

            if (end != null)
            {
                var cliEnd = end.Cliente;
                var endMongo = (await _enderecoCollection.FindAsync(x => x.RelationalId == end.Id.ToString()))?.FirstOrDefault();
                if (endMongo is not null)
                {
                    endMongo.Logradouro = end.Logradouro;
                    endMongo.Estado = end.Estado;
                    endMongo.RelationalId = end.Id.ToString();
                    endMongo.Cliente = new ClientesMongo
                    {
                        CPF = cliEnd.CPF,
                        Nome = cliEnd.Nome,
                        RelationalId = cliEnd.Id.ToString()
                    };

                    await _enderecoCollection.DeleteOneAsync(x => x.Id == endMongo.Id);
                    await _enderecoCollection.InsertOneAsync(endMongo);

                    //var endMongoId = endMongo.Id;
                    //var atualizacao = Builders<EnderecoMongo>.Update.Set(_ => _, endMongo);
                    //await _enderecosCollection.UpdateOneAsync(_ => _.Id == endMongoId, atualizacao);
                }
            }
        }
    }
}
