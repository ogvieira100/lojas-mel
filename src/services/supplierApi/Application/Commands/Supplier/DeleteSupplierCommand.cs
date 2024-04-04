using buildingBlocksCore.Mediator.Messages;
namespace supplierApi.Application.Commands.Supplier
{
    public class DeleteSupplierCommand : Command<object>
    {
        public Guid Id { get; set; }
    }
}
