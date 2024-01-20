using buildingBlocksCore.Data.ReadData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Repository
{
    public class ProdutoMongoRepository : BaseRepositoryMongo<ProdutoMongo>,
        IProdutoMongoRepository
    {
        public ProdutoMongoRepository(MongoContext mongoContext,
            IRepositoryConsultMongo<ProdutoMongo> baseConsultRepositoryMongo)
            : base(mongoContext, baseConsultRepositoryMongo)
        {


        }
    }
}
