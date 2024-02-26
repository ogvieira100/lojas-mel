using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class Fornecedor : EntityDataBase
    {
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public virtual IEnumerable<Endereco> Enderecos { get; set; }

        public virtual IEnumerable<Nota> Notas { get; set; }

        public virtual IEnumerable<Pedido> Pedidos { get; set; }

        public Fornecedor()
        {
            Notas = new List<Nota>();
            Pedidos = new List<Pedido>();
            Enderecos = new List<Endereco>();   
        }
    }
}
