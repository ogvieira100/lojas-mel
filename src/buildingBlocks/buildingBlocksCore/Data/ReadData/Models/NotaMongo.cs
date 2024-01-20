using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class NotaMongo : BaseMongo
    {
        public string FornecedorId { get; set; }
        public FornecedorMongo Fornecedor { get; set; }
        public string ClienteId { get; set; }
        public ClientesMongo Cliente { get; set; }
        public List<NotaItensMongo> NotaItens { get; set; }
        public string Observation { get; set; }
        public string Numero { get; set; }

        public NotaMongo()
        {
            NotaItens = new List<NotaItensMongo>();
            TableName = "nota";
        }
    }
}
