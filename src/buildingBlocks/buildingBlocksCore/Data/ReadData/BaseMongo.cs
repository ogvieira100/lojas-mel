using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData
{
    public class BaseMongo
    {
        public string Id { get; set; }

        public string RelationalId { get; set; }

        public string TableName { get; set; }
    }
}
