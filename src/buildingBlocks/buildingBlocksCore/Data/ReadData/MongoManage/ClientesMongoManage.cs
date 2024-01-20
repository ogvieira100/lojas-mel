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
    public class ClientesMongoManage : IClientesMongoManage
    {
        readonly IMongoCollection<ClientesMongo> _clientesCollection;
        readonly IMongoCollection<EnderecoMongo> _enderecoCollection;
        public ClientesMongoManage(MongoContext contextMongo)
        {
            _clientesCollection = contextMongo.DB.GetCollection<ClientesMongo>(new ClientesMongo().TableName);
            _enderecoCollection = contextMongo.DB.GetCollection<EnderecoMongo>(new EnderecoMongo().TableName);
        }

        public async Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Cliente>> clientes)
        {

            foreach (var cliente in clientes)
            {

                switch (cliente.Item1)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        await DeleteAsync(cliente.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        await UpdateAsync(cliente.Item2);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        await InsertAsync(cliente.Item2);
                        break;
                    default:
                        break;
                }

            }
        }

        private async Task DeleteAsync(Cliente cli)
        {
            if (cli != null)
            {
                var cliMongoDelete = (await _clientesCollection.FindAsync(x => x.RelationalId == cli.Id.ToString()))?.FirstOrDefault();
                if (cliMongoDelete != null)
                {
                    /*deletar a cascata nesse case endereço não existe sem cliente */
                    foreach (var endMongo in cliMongoDelete.Enderecos)
                    {
                        await _enderecoCollection.DeleteOneAsync(x => x.RelationalId == endMongo.RelationalId);
                    }
                    await _clientesCollection.DeleteOneAsync(x => x.RelationalId == cli.Id.ToString());
                }
            }
        }

        async Task UpdateAsync(Cliente cliente)
        {
            var cli = cliente;
            var cliMongo = (await _clientesCollection.FindAsync(x => x.RelationalId == cli.Id.ToString()))?.FirstOrDefault();
            if (cliMongo is not null
                && cli is not null)
            {
                cliMongo.CPF = cli.CPF;
                cliMongo.RelationalId = cli.Id.ToString();
                cliMongo.Nome = cli.Nome;
                cliMongo.Enderecos = cli
                                    .Enderecos
                                    .Select(x => new EnderecoMongo
                                    {
                                        Logradouro = x.Logradouro,
                                        Estado = x.Estado,
                                        Cliente = new ClientesMongo
                                        {
                                            CPF = cli.CPF,
                                            Nome = cli.Nome
                                        }

                                    }).ToList();

                await _clientesCollection.DeleteOneAsync(x => x.Id == cliMongo.Id);
                await _clientesCollection.InsertOneAsync(cliMongo);

                //var cliMongoId = cliMongo.Id;
                //var atualizacao = Builders<ClientesMongo>.Update.Set(_ => _, cliMongo);
                //await _clientesCollection.UpdateOneAsync(_ => _.Id == cliMongoId, atualizacao);
            }
        }

        async Task InsertAsync(Cliente cliente)
        {
            var clienteMongo = new ClientesMongo();
            clienteMongo.CPF = cliente.CPF;
            clienteMongo.Nome = cliente.Nome;
            clienteMongo.RelationalId = cliente.Id.ToString() ?? "";

            foreach (var end in cliente.Enderecos)
            {
                var endMongo = new EnderecoMongo();
                endMongo.Estado = end.Estado;
                endMongo.Logradouro = end.Logradouro;
                endMongo.RelationalId = end.Id.ToString();

                var cliEndMongo = new ClientesMongo();
                cliEndMongo.CPF = clienteMongo.CPF;
                cliEndMongo.RelationalId = clienteMongo.RelationalId;
                cliEndMongo.Nome = clienteMongo.Nome;
                endMongo.Cliente = cliEndMongo;


                clienteMongo.Enderecos.Add(endMongo);
            }
            await _clientesCollection.InsertOneAsync(clienteMongo);
        }
    }
}
