using buildingBlocksCore.Data.ReadData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Repository
{
    public class RepositoryConsultMongo<TEntity>
        : IRepositoryConsultMongo<TEntity> where TEntity : BaseMongo
    {

        readonly MongoContext _mongoContext;

        public IMongoCollection<TEntity> MongoCollectionConsult { get; }
        public RepositoryConsultMongo(MongoContext mongoContext)
        {

            _mongoContext = mongoContext;
            MongoCollectionConsult = _mongoContext.DB
                            .GetCollection<TEntity>(((TEntity)Activator.CreateInstance(typeof(TEntity))).TableName);

        }

        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await MongoCollectionConsult.AsQueryable().Where(predicate).AnyAsync();


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await (await MongoCollectionConsult.FindAsync(new BsonDocument())).ToListAsync();

        public async Task<TEntity> GetByIdAsync(string id)
        => await (await MongoCollectionConsult.FindAsync(x => x.RelationalId == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        => await (await MongoCollectionConsult.FindAsync(predicate)).ToListAsync();

        public async Task<PagedDataResponse<TEntity>> PaginateAsync(PagedDataRequest pagedDataRequest,
                                                                    Expression<Func<TEntity, bool>>? predicate)
        {
            if (predicate is null)
                return await MongoCollectionConsult.Find(new BsonDocument()).PaginateAsync(pagedDataRequest, null);

            return await MongoCollectionConsult.Find(predicate).PaginateAsync(pagedDataRequest, null);

        }

        public async Task<IEnumerable<TEntity>> SearchTextAsync(string text)
        {
            var vvv = MongoCollectionConsult.Find(new BsonDocument());

            var lists = new List<TEntity>();

            var filter = Builders<TEntity>.Filter.Text(text);


            await MongoCollectionConsult
                .AsQueryable()
                .Where(_ => filter.Inject())
                .ForEachAsync(d => lists.Add(d));

            return lists;
        }

    }
}
