using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using FluentValidation;
using MediatR;

namespace supplierApi.Application.Commands.Supplier
{
    public class SupplierCommandHandler :
        IRequestHandler<InsertSupplierCommand, ResponseCommad<InsertSupplierResponseCommand>>,
        IRequestHandler<UpdateSupplierCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteSupplierCommand, ResponseCommad<object>>
    {
        readonly IBaseRepository<Fornecedor> _SupplierRepository;
        readonly IMapper _mapper;
        readonly buildingBlocksCore.Identity.IUser _user;
        readonly LNotifications _notifications;
        readonly IMediatorHandler _mediatorHandler;
        readonly IValidator<InsertSupplierCommand> _validatorInsertSupplierCommand;
        readonly IValidator<UpdateSupplierCommand> _validatorUpdateSupplierCommand;
        readonly ILogger<SupplierCommandHandler> _logger;

        public SupplierCommandHandler(IMediatorHandler mediatorHandler,
                                     LNotifications notifications,
                                     ILogger<SupplierCommandHandler> logger,
                                     IValidator<InsertSupplierCommand> validatorInsertSupplierCommand,
                                     IValidator<UpdateSupplierCommand> validatorUpdateSupplierCommand,
                                     IBaseRepository<Fornecedor> SupplierRepository,
            buildingBlocksCore.Identity.IUser user,
            IMapper mapper)
        {
            _mapper = mapper;
            _user = user;
            _logger = logger;
            _validatorInsertSupplierCommand = validatorInsertSupplierCommand;
            _mediatorHandler = mediatorHandler;
            _notifications = notifications;
            _validatorUpdateSupplierCommand = validatorUpdateSupplierCommand;
            _SupplierRepository = SupplierRepository;
        }



        public Task<ResponseCommad<object>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCommad<object>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseCommad<InsertSupplierResponseCommand>> Handle(InsertSupplierCommand request, CancellationToken cancellationToken)
        {
            var resp = new ResponseCommad<InsertSupplierResponseCommand>();
            var supllierSearch = (await _SupplierRepository._repositoryConsult.SearchAsync(x => x.CNPJ == request.CNPJ.OnlyNumbers()))?.FirstOrDefault();
            if (supllierSearch != null)
            {


            }
            return resp;
        }
    }
}
