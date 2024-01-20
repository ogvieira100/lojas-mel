using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData.Models
{
    public class FornecedorMongo : BaseMongo
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }

        public FornecedorMongo()
        {
            TableName = "fornecedor";
        }
    }
}
