using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvChat(IChannel channel, Message message)
        {
            SChatMessage msg = message as SChatMessage;
            
            
        }
    }
}
