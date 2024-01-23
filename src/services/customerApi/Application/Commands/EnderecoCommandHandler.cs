using buildingBlocksCore.Mediator.Messages;
using MediatR;

namespace customerApi.Application.Commands
{
    public class EnderecoCommandHandler :
        IRequestHandler<InsertEnderecoCommand, ResponseCommad<InsertEnderecoResponseCommand>>,
        IRequestHandler<UpdateEnderecoCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteEnderecoCommand, ResponseCommad<object>>
    {
        public Task<ResponseCommad<InsertEnderecoResponseCommand>> Handle(InsertEnderecoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCommad<object>> Handle(UpdateEnderecoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCommad<object>> Handle(DeleteEnderecoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
