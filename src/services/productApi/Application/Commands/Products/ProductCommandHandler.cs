using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.Repository;
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
        readonly IProdutoMongoRepository _produtoMongoRepository;
        readonly IPedidoMongoRepository _pedidoMongoRepository;
        readonly INotaMongoRepository _notaMongoRepository;

        public ProductCommandHandler(IBaseRepository<Produto> produtoRepository,
            IMapper mapper, IUser user,
            LNotifications notifications,
            IPedidoMongoRepository pedidoMongoRepository,
            INotaMongoRepository notaMongoRepository,
            IMediatorHandler mediatorHandler,
            IProdutoMongoRepository produtoMongoRepository,
            IValidator<InsertProductCommand> validatorInsertProductCommand,
            IValidator<UpdateProductCommand> validatorUpdateProductCommand,
            ILogger<ProductCommandHandler> logger)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _user = user;
            _notaMongoRepository = notaMongoRepository;
            _pedidoMongoRepository = pedidoMongoRepository;
            _notifications = notifications;
            _mediatorHandler = mediatorHandler;
            _validatorInsertProductCommand = validatorInsertProductCommand;
            _validatorUpdateProductCommand = validatorUpdateProductCommand;
            _logger = logger;
            _produtoMongoRepository = produtoMongoRepository;
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

            var productSeach = (await _produtoMongoRepository.RepositoryConsultMongo.SearchAsync(x => x.Descricao == request.Descricao))?.FirstOrDefault();

            if (productSeach != null)
            {
                _notifications.Add(new LNotification
                {
                    Message = "Atenção descrição do produto já existe"
                });
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass
                {
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

            var productSearch = (await _produtoMongoRepository.RepositoryConsultMongo.SearchAsync(x => x.Descricao == request.Descricao && x.RelationalId != request.Id.ToString()))?.FirstOrDefault();
            if (productSearch != null)
            {
                _notifications.Add(new LNotification
                {
                    Message = "Atenção descrição do produto já existe"
                });
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = "Atenção descrição do produto já existe",
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Alerta,
                    Processo = Processo.AtualizarProduto
                });

                return res;
            }

            var productSave = (await _produtoMongoRepository.RepositoryConsultMongo.SearchAsync(x => x.RelationalId == request.Id.ToString()))?.FirstOrDefault();
            if (productSave != null)
            {
                var produtoAtualizar = new Produto();
                produtoAtualizar.Descricao = request.Descricao;
                produtoAtualizar.Id = new Guid(productSave.RelationalId);
                _produtoRepository.Update(produtoAtualizar);
                await _produtoRepository.unitOfWork.CommitAsync();
            }
            else
            {
                _notifications.Add(new LNotification
                {
                    Message = "Atenção produto inexistente verifique."
                });
                res.Notifications.AddRange(_notifications);
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = "Atenção produto inexistente verifique",
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Alerta,
                    Processo = Processo.AtualizarProduto
                });

            }
            return res;
        }

        public async Task<ResponseCommad<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var resp = new ResponseCommad<object>();
            var ProcessoId = Guid.NewGuid();

            var pedido = (await _pedidoMongoRepository
                    .RepositoryConsultMongo
                    .SearchAsync(x => x.PedidoItens.Any(p => p.ProdutoId == request.Id.ToString())));

            var nota = (await _notaMongoRepository
                   .RepositoryConsultMongo
                   .SearchAsync(x => x.NotaItens.Any(p => p.ProdutoId == request.Id.ToString())));


            if (pedido != null && pedido.Any()
                || nota != null && nota.Any())
            {
                _notifications.Add(new LNotification
                {
                    Message = "Atenção produto tem notas ou pedidos anexados não pode ser deletado."
                });
                resp.Notifications.AddRange(_notifications);

                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Product,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = "Atenção produto inexistente verifique",
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Alerta,
                    Processo = Processo.AtualizarProduto
                });
            }
            else
            { 
                    
            }
            return resp;

        }
    }
}
