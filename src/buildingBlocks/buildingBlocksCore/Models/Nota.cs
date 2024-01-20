using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class Nota : EntityDataBase
    {
        public virtual Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Guid FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual List<NotaItens> NotaItens { get; set; }
        public string? Observacao { get; set; }
        public string Numero { get; set; }

        public Nota()
        {
            NotaItens = new List<NotaItens>();
        }
    }
}
