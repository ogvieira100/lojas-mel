using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class NotaItens : EntityDataBase
    {
        public int Qtd { get; set; }
        public virtual Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Price { get; set; }
        public virtual Guid NotaId { get; set; }
        public virtual Nota Nota { get; set; }
    }
}
