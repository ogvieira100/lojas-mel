using buildingBlocksCore.Data.PersistData.Context;
using buildingBlocksCore.Data.PersistData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationContext _dbContext;

        public UnitOfWork(ApplicationContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<bool> CommitAsync() => await _dbContext.SaveChangesAsync() > 0;

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
