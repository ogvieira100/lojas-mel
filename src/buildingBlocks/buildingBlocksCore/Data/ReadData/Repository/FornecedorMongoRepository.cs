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
    public class FornecedorMongoRepository : BaseRepositoryMongo<FornecedorMongo>, IFornecedorMongoRepository
    {
        public FornecedorMongoRepository(MongoContext mongoContext, IRepositoryConsultMongo<FornecedorMongo> baseConsultRepositoryMongo) : base(mongoContext, baseConsultRepositoryMongo)
        {

        }
    }
}
