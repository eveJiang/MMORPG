using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            CBuyMessage request = message as CBuyMessage;
            Console.Write("ttttt");            
        }
    }
}
