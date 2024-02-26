using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;

namespace supplierApi.Application.Commands.Enderecos
{
    public class InsertEnderecoCommand : Command<InsertEnderecoResponseCommand>
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public UF Estado { get; set; }
    }
}
