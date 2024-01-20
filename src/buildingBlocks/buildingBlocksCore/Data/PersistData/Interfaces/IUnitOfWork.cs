using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        Task<bool> CommitAsync();
    }
}
