using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class PedidoItensMongo : BaseMongo
    {
        [JsonPropertyName("qtd")]
        public int Qtd { get; set; }

        [JsonPropertyName("produtoId")]
        public string ProdutoId { get; set; }

        [JsonPropertyName("produto")]
        public ProdutoMongo Produto { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("pedidoId")]
        public string PedidoId { get; set; }

        [JsonPropertyName("pedido")]
        public PedidoMongo Pedido { get; set; }

        public PedidoItensMongo()
        {
            TableName = "pedidoitens";
        }
    }
}
