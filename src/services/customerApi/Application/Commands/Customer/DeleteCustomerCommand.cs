using buildingBlocksCore.Mediator.Messages;

namespace customerApi.Application.Commands.Customer
{
    public class DeleteCustomerCommand : Command<object>
    {
        public Guid Id { get; set; }

        public Guid? UserDeleteId { get; set; }
    }
}
