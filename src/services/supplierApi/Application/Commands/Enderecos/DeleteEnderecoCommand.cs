using buildingBlocksCore.Mediator.Messages;

namespace supplierApi.Application.Commands.Enderecos
{
    public class DeleteEnderecoCommand : Command<object>
    {
        public Guid Id { get; set; }
    }
}
