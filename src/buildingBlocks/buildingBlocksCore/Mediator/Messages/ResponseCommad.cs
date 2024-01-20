using buildingBlocksCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Mediator.Messages
{
    public class ResponseCommad<T> where T : class
    {
        public T Response { get; set; }

        public LNotifications Notifications { get; set; }

        public ResponseCommad()
        {
                Notifications = new LNotifications();   
        }

    }
}
