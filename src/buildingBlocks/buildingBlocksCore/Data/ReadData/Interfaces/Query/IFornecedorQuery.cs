using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Interfaces.Query
{
    public interface IFornecedorQuery
    {
        Task<FornecedorMongo> GetFornecedorMongoByRelationId(string relationalId);

        Task<PagedDataResponse<FornecedorMongo>> PagedFornecedores(FornecedorPagedRequest fornecedorPagedRequest);
    }
}
