using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using MediatR;

namespace customerApi.Application.Commands
{
    public class EnderecoCommandHandler :
        IRequestHandler<InsertEnderecoCommand, ResponseCommad<InsertEnderecoResponseCommand>>,
        IRequestHandler<UpdateEnderecoCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteEnderecoCommand, ResponseCommad<object>>
    {

        readonly IBaseRepository<Endereco>  _enderecoRepository;
        readonly IMapper _mapper;
        readonly IUser _user;
        readonly LNotifications _notifications;


        public EnderecoCommandHandler(IBaseRepository<Endereco> enderecoRepository,
            IMapper mapper,
            IUser user, 
            LNotifications notifications)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
            _user = user;
            _notifications = notifications;
        }

        public async Task<ResponseCommad<InsertEnderecoResponseCommand>> Handle(InsertEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            var respCommand = new ResponseCommad<InsertEnderecoResponseCommand>();
           // if (request.Logradouro)


            return respCommand;
        }

        public async Task<ResponseCommad<object>> Handle(UpdateEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseCommad<object>> Handle(DeleteEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
