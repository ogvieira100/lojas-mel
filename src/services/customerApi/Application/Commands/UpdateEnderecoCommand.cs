using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;

namespace customerApi.Application.Commands
{
    public class UpdateEnderecoCommand : Command<object>
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public UF Estado { get; set; }
    }
}
