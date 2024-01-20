using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using buildingBlocksCore.Models.Request;
using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Query
{
    public class NotaQuery : INotaQuery
    {

        readonly INotaMongoRepository _notaMongoRepository;


        public NotaQuery(INotaMongoRepository notaMongoRepository)
        {
            _notaMongoRepository = notaMongoRepository;
        }

        public Task<NotaMongo> GetNotaByRelationalId(string relationalId)
        {
            throw new NotImplementedException();
        }

        public Task<NotaMongo> GetNotaUpdateByRelationalId(string relationalId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedDataResponse<NotaMongo>> PagedNotas(NotaPagedRequest clientePagedRequest)
         => await _notaMongoRepository.RepositoryConsultMongo.PaginateAsync(clientePagedRequest, null);
    }
}
