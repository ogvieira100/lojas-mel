using buildingBlocksCore.Mediator.Messages;
using customerApi.Application.Commands.Enderecos;

namespace customerApi.Application.Commands.Customer
{
    public class UpdateCustomerCommand : Command<object>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public IEnumerable<InsertEnderecoCommand> InsertEnderecos { get; set; }

        public IEnumerable<UpdateEnderecoCommand> UpdateEnderecos { get; set; }

        public UpdateCustomerCommand()
        {
            InsertEnderecos = new List<InsertEnderecoCommand>();
            UpdateEnderecos = new List<UpdateEnderecoCommand>();

        }
    }
}
