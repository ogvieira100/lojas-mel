﻿
using buildingBlocksCore.Mediator.Messages.Integration;
using buildingBlocksCore.Mediator;
using buildingBlocksMessageBus.Interfaces;
using customerApi.Application.Commands;

namespace customerApi.Services
{
    public class DeleteCustomerIntegrationHandler : BackgroundService
    {
        readonly IServiceProvider _serviceProvider;
        readonly IMessageBusRabbitMq _messageBusRabbitMq;

        public DeleteCustomerIntegrationHandler(IServiceProvider serviceProvider,
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

        void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        void SetResponder()
        {
            _messageBusRabbitMq.SubscribeAsync<UserDeletedIntegrationEvent>(
                new buildingBlocksMessageBus.Models.PropsMessageQueeDto { Queue = "QueeUserDeleted" },
               DeleteCustomerUserAsync
                );
        }

        async Task DeleteCustomerUserAsync(UserDeletedIntegrationEvent request)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceArchive = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                    var ret = await serviceArchive.SendCommand<DeleteCustomerCommand, object>(
                    new DeleteCustomerCommand
                    {
                        Id = request.Id,
                        UserDeleteId = request.UserDeleteId,  
                    });
                }
            }
            catch (Exception ex) { }
        }
    }
}
