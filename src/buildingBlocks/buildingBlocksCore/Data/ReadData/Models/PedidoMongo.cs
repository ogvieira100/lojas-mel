using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class PedidoMongo : BaseMongo
    {

        public string ClienteId { get; set; }
        public ClientesMongo Cliente { get; set; }
        public string FornecedorId { get; set; }

        public FornecedorMongo Fornecedor { get; set; }
        public List<PedidoItensMongo> PedidoItens { get; set; }
        public string Observation { get; set; }

        public PedidoMongo()
        {
            PedidoItens = new List<PedidoItensMongo>();
            TableName = "pedido";
        }

    }
}
