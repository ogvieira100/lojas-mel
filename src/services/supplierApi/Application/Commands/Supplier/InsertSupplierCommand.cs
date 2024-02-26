using buildingBlocksCore.Mediator.Messages;

namespace supplierApi.Application.Commands.Supplier
{//
    public class InsertSupplierCommand:Command<InsertSupplierResponseCommand>
    {
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
    }
}
