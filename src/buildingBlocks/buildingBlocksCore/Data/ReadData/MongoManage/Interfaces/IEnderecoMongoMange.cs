using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.MongoManage.Interfaces
{
    public interface IEnderecoMongoMange
    {
        Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Endereco>> enderecos);
    }
}
