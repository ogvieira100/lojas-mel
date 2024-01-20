using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Mediator.Messages.Integration;
using buildingBlocksCore.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task PublishEvent<T>(T evento) where T : Event
        {
            /*verificar se é evento de integração para logar no kibana*/
            if (evento is IntegrationEvent)
            { 

            
            }
            await _mediator.Publish(evento);
        }

        public async Task<ResponseCommad<R>> SendCommand<T,R>(T comando) where T : Command<R> where R : class
        {
            return await _mediator.Send(comando);
        }
    }
}
