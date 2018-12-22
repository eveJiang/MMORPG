using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvMyMarket(IChannel channel, Message message)
        {
            CMyMarket request = message as CMyMarket;
            SMyMarket reply = new SMyMarket();
            reply.items = Database.Instance.GetMyMarket(request.dbid);
            channel.Send(reply);
        }
    }
}

