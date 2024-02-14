using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using buildingBlocksCore.Validations.Extension;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

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
            var res = new ResponseCommad<InsertProductResponseCommand>();
            var validation = await _validatorInsertProductCommand.ValidateAsync(request);
            var ProcessoId = Guid.NewGuid();

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
            {
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = JsonConvert.SerializeObject(_notifications),
                    EObjetoJson = true,
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Erro,
                    Processo = Processo.InserirProduto
                });

                return res;
            }

            var productSearch = (await _produtoRepository._repositoryConsult.SearchAsync(x => x.Descricao == request.Descricao ))?.FirstOrDefault();
            if (productSearch != null)
            {
                _notifications.Add(new LNotification {
                    Message = "Atenção descrição do produto já existe"
                });
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass{
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = "Atenção descrição do produto já existe",
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Alerta,
                    Processo = Processo.InserirProduto
                });

                return res;
            }
            var produtoAdicionar = new Produto();
            produtoAdicionar.Descricao = request.Descricao;
            await _produtoRepository.AddAsync(produtoAdicionar);
            await _produtoRepository.unitOfWork.CommitAsync();
            return res;
        }

        public async Task<ResponseCommad<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var res = new ResponseCommad<object>();
            var validation = await _validatorUpdateProductCommand.ValidateAsync(request);
            var ProcessoId = Guid.NewGuid();

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
            {
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = JsonConvert.SerializeObject(_notifications),
                    EObjetoJson = true,
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Erro,
                    Processo = Processo.AtualizarProduto
                });

                return res;
            }

            return res;
        }

        public async  Task<ResponseCommad<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
