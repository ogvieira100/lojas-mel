using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class Pedido : EntityDataBase
    {

        public virtual Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Guid FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual List<PedidoItens> PedidoItens { get; set; }
        public string? Observacao { get; set; }
    }
}
