using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using FluentValidation;
using MediatR;

namespace productApi.Application.Commands.Products
{
    public class ProductCommandHandler :
        IRequestHandler<InsertProductCommand, ResponseCommad<InsertProductResponseCommand>>,
        IRequestHandler<UpdateProductCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteProductCommand, ResponseCommad<object>>
    {

        readonly IBaseRepository<Produto> _produtoRepository;
        readonly IMapper _mapper;
        readonly buildingBlocksCore.Identity.IUser _user;
        readonly LNotifications _notifications;
        readonly IMediatorHandler _mediatorHandler;
        readonly IValidator<InsertProductCommand> _validatorInsertProductCommand;
        readonly IValidator<UpdateProductCommand> _validatorUpdateProductCommand;
        readonly ILogger<ProductCommandHandler> _logger;
 
        public ProductCommandHandler(IBaseRepository<Produto> produtoRepository, 
            IMapper mapper, IUser user,
            LNotifications notifications,
            IMediatorHandler mediatorHandler,
            IValidator<InsertProductCommand> validatorInsertProductCommand, 
            IValidator<UpdateProductCommand> validatorUpdateProductCommand,
            ILogger<ProductCommandHandler> logger)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _user = user;
            _notifications = notifications;
            _mediatorHandler = mediatorHandler;
            _validatorInsertProductCommand = validatorInsertProductCommand;
            _validatorUpdateProductCommand = validatorUpdateProductCommand;
            _logger = logger;
        }

        public async Task<ResponseCommad<InsertProductResponseCommand>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseCommad<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async  Task<ResponseCommad<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
