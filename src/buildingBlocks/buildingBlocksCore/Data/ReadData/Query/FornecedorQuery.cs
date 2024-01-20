using buildingBlocksCore.Data.ReadData;
using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models;
using buildingBlocksCore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Query
{
    public class FornecedorQuery : IFornecedorQuery
    {

        readonly IFornecedorMongoRepository _fornecedorMongoRepository;

        public FornecedorQuery(IFornecedorMongoRepository fornecedorMongoRepository)
        {
            _fornecedorMongoRepository = fornecedorMongoRepository;
        }

        public async Task<FornecedorMongo> GetFornecedorMongoByRelationId(string relationalId)
        => await _fornecedorMongoRepository.RepositoryConsultMongo.GetByIdAsync(relationalId);

        public async Task<PagedDataResponse<FornecedorMongo>> PagedFornecedores(FornecedorPagedRequest clientePagedRequest)
        => await _fornecedorMongoRepository.RepositoryConsultMongo.PaginateAsync(clientePagedRequest, null);
    }
}
