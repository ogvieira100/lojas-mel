
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Mediator.Messages.Integration;
using buildingBlocksMessageBus.Interfaces;
using customerApi.Application.Commands;

namespace customerApi.Services
{
    public class InsertCustomerIntegrationHandler : BackgroundService
    {
        readonly IServiceProvider _serviceProvider;
        readonly IMessageBusRabbitMq _messageBusRabbitMq;

        public InsertCustomerIntegrationHandler(IServiceProvider serviceProvider,
               IMessageBusRabbitMq messageBusRabbitMq)
        {
            _serviceProvider = serviceProvider;
            _messageBusRabbitMq = messageBusRabbitMq;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private void SetResponder()
        {
            _messageBusRabbitMq.RpcSendResponseReceiveRequestAsync<UserInsertedIntegrationEvent, buildingBlocksCore.Mediator.Messages.ResponseMessage>(new buildingBlocksMessageBus.Models.PropsMessageQueeDto { Queue = "RPCUserInserted", Durable = false },
                ReceiveUserAsync);
        }

        private async Task<ResponseMessage> ReceiveUserAsync(UserInsertedIntegrationEvent request)
        {
            var resp = new ResponseMessage();
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceArchive = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                    var commandInserted = new InsertCustomerCommand();
                    commandInserted.UserInsertedId = request.UserInserted;
                    commandInserted.Nome = request.Nome;
                    commandInserted.CPF = request.CPF;
                    commandInserted.Email = request.Email;
                    commandInserted.Id = request.UserId;
                    var respCommand = await serviceArchive.SendCommand<InsertCustomerCommand, InsertCustomerResponseCommad>(commandInserted);
                    if (respCommand.Notifications.Any())
                        resp.Notifications.AddRange(respCommand.Notifications);
                }
            }
            catch (Exception ex)
            {
                resp.Notifications.Add(new buildingBlocksCore.Utils.LNotification { Message = ex.Message });
            }
            return resp;
        }
    }
}
