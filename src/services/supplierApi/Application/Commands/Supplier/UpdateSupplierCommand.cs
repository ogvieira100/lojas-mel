using buildingBlocksCore.Mediator.Messages;

namespace supplierApi.Application.Commands.Supplier
{
    public class UpdateSupplierCommand : Command<object>
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
    }
}
