using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models
{

    public enum UF
    {
        SP = 1,
        RJ = 2,
        MG = 3
    }
    public enum TipoEndereco
    { 
    
        Rua = 1 ,
        Avenida = 2 
    
    }
    public class Endereco : EntityDataBase
    {
        public virtual Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Guid FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public string Logradouro  { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public UF Estado { get; set; }

        public Endereco()
        {
                
        }
    }
}
