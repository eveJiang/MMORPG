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
            reply.gold = Database.Instance.GetGoldCoins(request.userdbid);
            reply.silver = Database.Instance.GetSilverCoins(request.userdbid);
            channel.Send(reply);
        }
    }
}

