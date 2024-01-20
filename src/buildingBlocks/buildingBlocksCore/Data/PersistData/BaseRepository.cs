using buildingBlocksCore.Data.PersistData.Context;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData
{
    public  class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityDataBase
    {

        public IUnitOfWork unitOfWork { get; }

        public IRepositoryConsult<TEntity> _repositoryConsult { get; protected set; }

        readonly DbSet<TEntity> DbSet;

        readonly ApplicationContext _applicationContext;
        public BaseRepository(IUnitOfWork _unitOfWork,
                                 IRepositoryConsult<TEntity> repositoryConsult,
                                 ApplicationContext applicationContext)
        {

            _applicationContext = applicationContext;
            unitOfWork = _unitOfWork;
            _repositoryConsult = repositoryConsult; 
            DbSet = _applicationContext.Set<TEntity>();

        }
        public void Add(TEntity entity) => DbSet.Add(entity);

        public void Dispose() => GC.SuppressFinalize(this);

        public void Remove(TEntity entity) => DbSet.Remove(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public async Task AddAsync(TEntity entidade) => await DbSet.AddAsync(entidade);

        public async Task AddAsync<T>(T entidade) where T : EntityDataBase => await _applicationContext.Set<T>().AddAsync(entidade);

        public void Remove<T>(T customer) where T : class
             => _applicationContext.Set<T>().Remove(customer);

    }
}
