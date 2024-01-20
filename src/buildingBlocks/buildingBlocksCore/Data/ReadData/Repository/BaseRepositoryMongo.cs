using buildingBlocksCore.Data.ReadData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Repository
{
    public class BaseRepositoryMongo<TEntity>
            : IBaseRepositoryMongo<TEntity> where TEntity : BaseMongo
    {

        readonly MongoContext _mongoContext;


        public BaseRepositoryMongo(MongoContext mongoContext,
            IRepositoryConsultMongo<TEntity> baseConsultRepositoryMongo)
        {

            RepositoryConsultMongo = baseConsultRepositoryMongo;
            _mongoContext = mongoContext;
            MongoCollectionPersist = _mongoContext.DB
                         .GetCollection<TEntity>(((TEntity)Activator.CreateInstance(typeof(TEntity))).TableName);

        }

        public IRepositoryConsultMongo<TEntity> RepositoryConsultMongo { get; }

        public IMongoCollection<TEntity> MongoCollectionPersist { get; }

        public void Add(TEntity entidade)
        => MongoCollectionPersist.InsertOne(entidade);

        public async Task AddAsync(TEntity entidade)
        => await MongoCollectionPersist.InsertOneAsync(entidade);

        public void AddMany(IEnumerable<TEntity> entidade)
        => MongoCollectionPersist.InsertMany(entidade);


        public async Task AddManyAsync(IEnumerable<TEntity> entidade)
        => await MongoCollectionPersist.InsertManyAsync(entidade);

        public void Dispose()
        => GC.SuppressFinalize(this);

        public void Remove(TEntity customer)
        => MongoCollectionPersist.DeleteOne(_ => _.RelationalId == customer.RelationalId);

        public async Task RemoveAsync(TEntity customer)
        => await MongoCollectionPersist.DeleteOneAsync(_ => _.RelationalId == customer.RelationalId);

        public void RemoveMany(IEnumerable<TEntity> customer)
        {
            var ids = customer.Select(x => x.RelationalId);
            MongoCollectionPersist.DeleteMany(_ => ids.Contains(_.RelationalId));
        }

        public void Update(TEntity customer)
        {

            Remove(customer);
            Add(customer);
            //var atualizacao = Builders<TEntity>.Update.Set(_ => _, customer);
            //MongoCollectionPersist.UpdateOne(_ => _.RelationalId == id, atualizacao);
        }

        public async Task UpdateAsync(TEntity customer)
        {

            await RemoveAsync(customer);
            await AddAsync(customer);
            //var atualizacao = Builders<TEntity>.Update.Set(_ => _, customer);
            //await MongoCollectionPersist.UpdateOneAsync(_ => _.RelationalId == id, atualizacao);
        }


    }
}
