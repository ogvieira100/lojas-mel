﻿using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.MongoManage.Interfaces
{
    public interface INotaMongoManage
    {
        Task ExecManager(List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Nota>> pedidos);
    }
}
