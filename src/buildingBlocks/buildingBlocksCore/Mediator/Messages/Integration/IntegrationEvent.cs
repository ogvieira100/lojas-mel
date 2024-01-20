using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Mediator.Messages.Integration
{

    public enum PositionEvent
    { 
    
        Start = 1,
        Proccess = 2,
        End = 3,
    
    }

    public enum TypeProcess
    { 
        
        IncluirUsuario = 1,
        DeletarUsuario = 2
    
    }
    public abstract class IntegrationEvent : Event
    {

        public PositionEvent PositionEvent { get; set; }
        public TypeProcess? TypeProcess { get; set; }
        public int Order { get; set; }
        public Guid UserId { get; set; }
        public IntegrationEvent() : base()   
        {
                    
        }

    }
}
