using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{
    public class Cliente : EntityDataBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }
        public virtual List<Endereco> Enderecos { get; set; }

        public virtual List<Nota> Notas { get; set; }

        public virtual List<Pedido> Pedidos { get; set; }

        public Cliente()
        {
            Enderecos = new List<Endereco>();
            Notas = new List<Nota>();
            Pedidos = new List<Pedido>();
        }


        public void SetId(Guid id)
        {
            Id = id;
        }

    }
}
