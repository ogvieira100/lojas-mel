using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Data.PersistData.Uow;
using buildingBlocksCore.Identity;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using MediatR;

namespace customerApi.Application.Commands
{
    public class CustomerCommandHandler :
        IRequestHandler<InsertCustomerCommand, ResponseCommad<InsertCustomerResponseCommad>>,
        IRequestHandler<UpdateCustomerCommand, ResponseCommad<object>>,
        IRequestHandler<DeleteCustomerCommand, ResponseCommad<object>>
    {
        readonly IBaseRepository<Cliente> _customerRepository;
        readonly IMapper _mapper;
        readonly IUser _user;
        readonly LNotifications _notifications;
        readonly IMediatorHandler _mediatorHandler;
        public CustomerCommandHandler(IMediatorHandler mediatorHandler, LNotifications notifications,IBaseRepository<Cliente> customerRepository,
            IUser user,
            IMapper mapper)
        {
            _mapper = mapper;
            _user = user;
            _mediatorHandler = mediatorHandler; 
            _notifications = notifications; 
            _customerRepository = customerRepository; 
        }
        public async Task<ResponseCommad<InsertCustomerResponseCommad>> Handle(InsertCustomerCommand request, 
            CancellationToken cancellationToken)
        {
            var res = new ResponseCommad<InsertCustomerResponseCommad>();
            res.Response = new InsertCustomerResponseCommad();  

            var customerCPF = (await _customerRepository._repositoryConsult.SearchAsync(x => x.CPF == request.CPF.OnlyNumbers()))?.FirstOrDefault();
            if (customerCPF != null)
            {
                res.Notifications.Add(new LNotification { Message = $"Atenção CPF já existente para o Cliente {customerCPF.Nome} com o status {customerCPF.Active} " });
                return res;
            }

            var customerSave = _mapper.Map<Cliente>(request);
            customerSave.SetId(request.Id);
            if (request.UserInsertedId != null)
                customerSave.UserInsertedId = request.UserInsertedId.Value;
            else 
                customerSave.UserInsertedId = _user.GetUserId();
            customerSave.DateRegister = DateTime.Now;
            await _customerRepository.AddAsync(customerSave);
            /**/
            foreach (var item in request.InsertEnderecos)
            {
                var endInsert =  await _mediatorHandler.SendCommand<InsertEnderecoCommand, InsertEnderecoResponseCommand>(item);
                if (endInsert.Response?.Endereco is not null)
                    customerSave.Enderecos.Add(endInsert.Response.Endereco);
            }
            if (!_notifications.Any())
              await _customerRepository.unitOfWork.CommitAsync();

            res.Response.Id = customerSave.Id;   
            return res;
        }

        public async Task<ResponseCommad<object>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var res = new ResponseCommad<object>();
            var customerCPF = (await _customerRepository._repositoryConsult.SearchAsync(x => x.Id != request.Id
                                                                                  && x.CPF == request.CPF.OnlyNumbers()))
                                                                                  ?.FirstOrDefault();
            if (customerCPF != null)
            {
                res.Notifications.Add(new LNotification { Message = $"Atenção CPF já existente para o Cliente {customerCPF.Nome} com o status {customerCPF.Active} " });
                return res;
            }

            var customerSearch = (await _customerRepository._repositoryConsult
                                                            .SearchAsync(x => x.Id == request.Id))?
                                                            .FirstOrDefault();

            if (customerSearch != null)
            {
                customerSearch.DateUpdate = DateTime.Now;
                customerSearch.UserUpdatedId = _user.GetUserAdm();
                customerSearch.Nome = request.Nome;
                customerSearch.Email = request.Email;
                customerSearch.CPF = request.CPF.OnlyNumbers();   
            }
            /**/
            foreach (var item in request.InsertEnderecos)
            {
                var endInsert = await _mediatorHandler.SendCommand<InsertEnderecoCommand, InsertEnderecoResponseCommand>(item);
                if (endInsert.Response?.Endereco is not null)
                    customerSearch.Enderecos.Add(endInsert.Response.Endereco);
            }
            foreach (var item in request.UpdateEnderecos)
                await _mediatorHandler.SendCommand<UpdateEnderecoCommand, object>(item);
            
            if (!_notifications.Any())
                await _customerRepository.unitOfWork.CommitAsync();
            return res;

        }

        public async Task<ResponseCommad<object>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var res = new ResponseCommad<object>();
            var customerSearch = (await _customerRepository._repositoryConsult
                                                           .SearchAsync(x => x.Id.ToString() == request.Id.ToString().ToLower()))?
                                                           .FirstOrDefault();

            if (customerSearch != null)
            {

                customerSearch.DeleteDate = DateTime.Now;
                if (request.UserDeleteId.HasValue)
                    customerSearch.UserDeletedId = request.UserDeleteId;
                else
                    customerSearch.UserDeletedId = _user.GetUserId();
                customerSearch.Active = false;
            }
            await _customerRepository.unitOfWork.CommitAsync();
            return res;

        }

    }
}
