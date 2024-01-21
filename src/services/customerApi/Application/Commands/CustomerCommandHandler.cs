using AutoMapper;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Data.PersistData.Uow;
using buildingBlocksCore.Identity;
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
        public CustomerCommandHandler(IBaseRepository<Cliente> customerRepository,
            IUser user,
            IMapper mapper)
        {
            _mapper = mapper;
            _user = user;   
            _customerRepository = customerRepository; 
        }
        public async Task<ResponseCommad<InsertCustomerResponseCommad>> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
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
