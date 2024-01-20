using buildingBlocksCore.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Mediator.Messages
{
    public class Command<T> : Message, IRequest<ResponseCommad<T>> where T :class 
    {



    }
}
