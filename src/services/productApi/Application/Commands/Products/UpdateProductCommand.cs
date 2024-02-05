using buildingBlocksCore.Mediator.Messages;

namespace productApi.Application.Commands.Products
{
    public class UpdateProductCommand:Command<object>
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
    }
}
