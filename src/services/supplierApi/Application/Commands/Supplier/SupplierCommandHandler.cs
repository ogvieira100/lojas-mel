using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using buildingBlocksCore.Validations.Extension;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

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
        readonly IFornecedorMongoRepository _fornecedorMongoRepository;
        readonly IValidator<InsertSupplierCommand> _validatorInsertSupplierCommand;
        readonly IValidator<UpdateSupplierCommand> _validatorUpdateSupplierCommand;
        readonly ILogger<SupplierCommandHandler> _logger;

        public SupplierCommandHandler(IMediatorHandler mediatorHandler,
                                     LNotifications notifications,
                                     ILogger<SupplierCommandHandler> logger,
                                     IFornecedorMongoRepository fornecedorMongoRepository,
                                     IValidator<InsertSupplierCommand> validatorInsertSupplierCommand,
                                     IValidator<UpdateSupplierCommand> validatorUpdateSupplierCommand,
                                     IBaseRepository<Fornecedor> SupplierRepository,
            buildingBlocksCore.Identity.IUser user,
            IMapper mapper)
        {
            _mapper = mapper;
            _user = user;
            _logger = logger;
            _fornecedorMongoRepository = fornecedorMongoRepository;
            _validatorInsertSupplierCommand = validatorInsertSupplierCommand;
            _mediatorHandler = mediatorHandler;
            _notifications = notifications;
            _validatorUpdateSupplierCommand = validatorUpdateSupplierCommand;
            _SupplierRepository = SupplierRepository;
        }



        public async Task<ResponseCommad<object>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var ProcessoId = Guid.NewGuid();
            var resp = new ResponseCommad<object>();

            _logger.Logar(new LogClass
            {
                Aplicacao = Aplicacao.Supplier,
                EstadoProcesso = EstadoProcesso.Processando,
                Msg = "Atualizando Fornecedor",
                ProcessoId = ProcessoId,
                TipoLog = TipoLog.Informacao,
                Processo = Processo.AtualizarFornecedor
            });

            var validation = await _validatorUpdateSupplierCommand.ValidateAsync(request);

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
            {
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Supplier,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = JsonConvert.SerializeObject(_notifications),
                    EObjetoJson = true,
                    ProcessoId = request.ProcessoId,
                    TipoLog = TipoLog.Erro,
                    Processo = Processo.AtualizarFornecedor
                });
                return resp;
            }


            return resp;
        }

        public async Task<ResponseCommad<object>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var resp = new ResponseCommad<object>();
            var ProcessoId = Guid.NewGuid();



            return resp;
        }

        public async Task<ResponseCommad<InsertSupplierResponseCommand>> Handle(InsertSupplierCommand request, CancellationToken cancellationToken)
        {
            var resp = new ResponseCommad<InsertSupplierResponseCommand>();
            var ProcessoId  = Guid.NewGuid();

            _logger.Logar(new LogClass
            {
                Aplicacao = Aplicacao.Supplier,
                EstadoProcesso = EstadoProcesso.Processando,
                Msg = "Inserindo Fornecedor",
                ProcessoId = ProcessoId,
                TipoLog = TipoLog.Informacao,
                Processo = Processo.InserirFornecedor
            });

            var validation = await _validatorInsertSupplierCommand.ValidateAsync(request);

            if (!validation.IsValid)
                _notifications.AddRange(validation.GetErrors().Select(x => new LNotification
                {
                    Message = x.ErrorMessage
                }));

            if (_notifications.Any())
            {
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Customer,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = JsonConvert.SerializeObject(_notifications),
                    EObjetoJson = true,
                    ProcessoId = request.ProcessoId,
                    TipoLog = TipoLog.Erro,
                    Processo = Processo.InserirUsuario
                });
                return resp;
            }


            var supllierSearch = (await _fornecedorMongoRepository
                .RepositoryConsultMongo
                .SearchAsync(x => x.CNPJ == request.CNPJ)).FirstOrDefault();
            if (supllierSearch != null)
            {
                _logger.Logar(new LogClass
                {
                    Aplicacao = Aplicacao.Supplier,
                    EstadoProcesso = EstadoProcesso.Processando,
                    Msg = "Fornecedor existente",
                    ProcessoId = ProcessoId,
                    TipoLog = TipoLog.Erro,
                    Processo = Processo.InserirFornecedor
                });
            }
            var novoSupplier = new Fornecedor();
            await _SupplierRepository.AddAsync(novoSupplier);

            return resp;
        }
    }
}
