using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvCoin(IChannel channel, Message message)
        {
            CCoinMessage request = message as CCoinMessage;
            SCoinMessage reply = new SCoinMessage();
            var conn = db.Instance.Connect();
            reply.gold = db.Instance.GetGoldCoins(request.userdbid, conn);
            reply.silver = db.Instance.GetSilverCoins(request.userdbid, conn);
            channel.Send(reply);
        }
    }
}

