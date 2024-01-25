using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using buildingBlocksCore.Validations.Extension;
using FluentValidation;
using MediatR;
using System.Collections.Generic;

namespace customerApi.Application.Commands.Enderecos
{
    public class EnderecoCommandHandler :
        IRequestHandler<InsertEnderecoCommand, ResponseCommad<InsertEnderecoResponseCommand>>,
        IRequestHandler<UpdateEnderecoCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteEnderecoCommand, ResponseCommad<object>>
    {

        readonly IBaseRepository<Endereco> _enderecoRepository;
        readonly IMapper _mapper;
        readonly IUser _user;
        readonly LNotifications _notifications;
        readonly IValidator<InsertEnderecoCommand> _validatorInsertEnderecoCommand;
        readonly IValidator<UpdateEnderecoCommand> _validatorUpdateEnderecoCommand;

        public EnderecoCommandHandler(IBaseRepository<Endereco> enderecoRepository,
            IMapper mapper,
            IUser user,
            IValidator<UpdateEnderecoCommand> validatorUpdateEnderecoCommand,
            IValidator<InsertEnderecoCommand> validator,
            LNotifications notifications)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
            _validatorInsertEnderecoCommand = validator;
            _user = user;
            _notifications = notifications;
            _validatorUpdateEnderecoCommand = validatorUpdateEnderecoCommand;
        }

        public async Task<ResponseCommad<InsertEnderecoResponseCommand>> Handle(InsertEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            var respCommand = new ResponseCommad<InsertEnderecoResponseCommand>();
            var validation = await _validatorInsertEnderecoCommand.ValidateAsync(request);

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
                return respCommand;

            respCommand.Response.Endereco = _mapper.Map<Endereco>(request);
            await _enderecoRepository.AddAsync(respCommand.Response.Endereco);
            return respCommand;
        }

        public async Task<ResponseCommad<object>> Handle(UpdateEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            var respCommand = new ResponseCommad<object>();
            var validation = await _validatorUpdateEnderecoCommand.ValidateAsync(request);

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
                return respCommand;

            var endUpdate = (await _enderecoRepository._repositoryConsult.SearchAsync(x => x.Id == request.Id))?.FirstOrDefault();
            if (endUpdate is not null)
            {
                endUpdate.Bairro = request.Bairro;
                endUpdate.Logradouro = request.Logradouro;
                endUpdate.Numero = request.Numero;
                endUpdate.TipoEndereco = request.TipoEndereco;
                endUpdate.Estado = request.Estado;
            }
            return respCommand;
        }

        public async Task<ResponseCommad<object>> Handle(DeleteEnderecoCommand request,
            CancellationToken cancellationToken)
        {
            var respCommand = new ResponseCommad<object>();
            var endUpdate = (await _enderecoRepository._repositoryConsult.SearchAsync(x => x.Id == request.Id))?.FirstOrDefault();
            if (endUpdate is not null)
            {
                endUpdate.UserDeletedId = _user.GetUserId();
                endUpdate.Active = false;
                endUpdate.DeleteDate = DateTime.Now;
            }
            return respCommand;
        }
    }
}
