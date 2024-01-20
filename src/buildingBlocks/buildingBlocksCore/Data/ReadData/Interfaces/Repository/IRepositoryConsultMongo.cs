using buildingBlocksCore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Interfaces.Repository
{
    public interface IRepositoryConsultMongo<TEntity> : IDisposable where TEntity : BaseMongo
    {
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> SearchTextAsync(string text);
        Task<TEntity> GetByIdAsync(string id);
        IMongoCollection<TEntity> MongoCollectionConsult { get; }
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedDataResponse<TEntity>> PaginateAsync(PagedDataRequest pagedDataRequest, Expression<Func<TEntity, bool>>? predicate);
    }
}
