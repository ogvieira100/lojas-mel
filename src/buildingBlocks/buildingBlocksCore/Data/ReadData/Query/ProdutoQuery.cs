using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Interfaces.Query;

namespace buildingBlocksCore.Data.ReadData.Query
{
    public class ProdutoQuery : IProdutoQuery
    {

        readonly IProdutoMongoRepository _produtoMongoRepository;

        public ProdutoQuery(IProdutoMongoRepository produtoMongoRepository)
        {
            _produtoMongoRepository = produtoMongoRepository;
        }

        public async Task<ProdutoMongo> GetProdutoMongoByRelationId(string relationalId)
        => await _produtoMongoRepository.RepositoryConsultMongo.GetByIdAsync(relationalId);

        public async Task<IEnumerable<ProdutoMongo>> GetProdutosMongoByRelationsIds(IEnumerable<string> relationalsIds)
        => await _produtoMongoRepository.RepositoryConsultMongo.SearchAsync(x => relationalsIds.Contains(x.RelationalId));


        public async Task<PagedDataResponse<ProdutoMongo>> PagedProdutos(ProdutoPagedRequest clientePagedRequest)
        => await _produtoMongoRepository.RepositoryConsultMongo.PaginateAsync(clientePagedRequest, null);
    }
}
