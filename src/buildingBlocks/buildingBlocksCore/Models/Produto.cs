using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class Produto : EntityDataBase
    {
        public string Descricao { get; set; }

        public virtual IEnumerable<PedidoItens> PedidoItens { get; set; }

        public virtual IEnumerable<NotaItens> NotaItens { get; set; }

        public Produto()
        {
            NotaItens = new List<NotaItens>();
            PedidoItens = new List<PedidoItens>();
        }
    }
}
