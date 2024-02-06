using buildingBlocksCore.Mediator.Messages;

namespace productApi.Application.Commands.Products
{
    public class DeleteProductCommand:Command<object>
    {
        public Guid Id { get; set; }
    }
}
