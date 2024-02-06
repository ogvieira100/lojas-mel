using buildingBlocksCore.Mediator.Messages;

namespace productApi.Application.Commands.Products
{
    public class InsertProductCommand:Command<InsertProductResponseCommand>
    {
        public string Descricao { get; set; }
    }
}
