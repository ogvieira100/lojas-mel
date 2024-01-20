using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class NotaItensMongo : BaseMongo
    {
        public int Qtd { get; set; }
        public string ProdutoId { get; set; }
        public ProdutoMongo Produto { get; set; }
        public decimal Price { get; set; }
        public string NotaId { get; set; }
        public NotaMongo Nota { get; set; }
        public NotaItensMongo()
        {
            TableName = "notaItens";
        }

    }
}
