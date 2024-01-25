using buildingBlocksCore.Mediator.Messages;

namespace customerApi.Application.Commands.Enderecos
{
    public class DeleteEnderecoCommand : Command<object>
    {
        public Guid Id { get; set; }
    }
}
