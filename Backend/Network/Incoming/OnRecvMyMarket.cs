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
            var conn = db.Instance.Connect();
            reply.items = db.Instance.GetMyMarket(request.dbid, conn);
            channel.Send(reply);
        }
    }
}

