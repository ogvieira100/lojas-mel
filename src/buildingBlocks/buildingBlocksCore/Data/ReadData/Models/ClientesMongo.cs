using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class ClientesMongo : BaseMongo
    {

        public string CPF { get; set; }
        public string Nome { get; set; }
        public List<EnderecoMongo> Enderecos { get; set; }
        public ClientesMongo()
        {
            Enderecos = new List<EnderecoMongo>();
            TableName = "clientes";
        }
    }
}
