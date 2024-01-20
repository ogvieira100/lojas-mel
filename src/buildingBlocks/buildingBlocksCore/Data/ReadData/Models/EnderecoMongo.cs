using buildingBlocksCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class EnderecoMongo : BaseMongo
    {
        public string Logradouro { get; set; }

        public string ClienteId { get; set; }

        public ClientesMongo Cliente { get; set; }
        public UF Estado { get; set; }

        public EnderecoMongo()
        {
            Cliente = new ClientesMongo();
            TableName = "endereco";
        }
    }
}
