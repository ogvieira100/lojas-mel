using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Interfaces.Query
{
    public interface INotaQuery
    {
        Task<NotaMongo> GetNotaByRelationalId(string relationalId);
        Task<NotaMongo> GetNotaUpdateByRelationalId(string relationalId);
        Task<PagedDataResponse<NotaMongo>> PagedNotas(NotaPagedRequest clientePagedRequest);

    }
}
