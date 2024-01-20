using buildingBlocksCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Mediator.Messages
{
    public class ResponseMessage
    {
        public LNotifications Notifications { get; set; }

        public ResponseMessage()
        {
                Notifications = new LNotifications();    
        }
    }
}
