using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class PedidoItens : EntityDataBase
    {
        public int Qtd { get; set; }
        public virtual Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Price { get; set; }
        public virtual Guid PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
