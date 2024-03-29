﻿using buildingBlocksCore.Data.PersistData.Context;
using buildingBlocksCore.Data.PersistData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData
{
    public class RepositoryConsult<TEntity> : IRepositoryConsult<TEntity> where TEntity : class
    {

        readonly ApplicationContext _Context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryConsult(ApplicationContext Context)
        {
            _Context = Context;
            DbSet = _Context.Set<TEntity>();
        }

        public void Dispose() => GC.SuppressFinalize(this);
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.AnyAsync(predicate);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();
        public async Task<TEntity> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);
        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();
        public IQueryable<TEntity> GetQueryable() => DbSet.AsQueryable();


    }
}
