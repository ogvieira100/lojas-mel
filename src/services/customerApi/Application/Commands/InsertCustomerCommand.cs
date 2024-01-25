using buildingBlocksCore.Mediator.Messages;

namespace customerApi.Application.Commands
{
    public class InsertCustomerCommand : Command<InsertCustomerResponseCommad>
    {
        public Guid? UserInsertedId { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public IEnumerable<InsertEnderecoCommand> InsertEnderecos { get; set; }
        public InsertCustomerCommand()
        {
            InsertEnderecos = new List<InsertEnderecoCommand>();

        }
    }
}
