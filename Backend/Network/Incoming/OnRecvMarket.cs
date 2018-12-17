using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvMarket(IChannel channel, Message message)
        {
            CMarketMessage request = message as CMarketMessage;
            SMarketMessage reply = new SMarketMessage();
            switch (request.option)
            {
                case "get":
                    reply.items = Database.Instance.GetMarket();
                    reply.option = "get";
                    break;
                case "sell":
                    Database.Instance.MarketSell(request.items[0]);
                    reply.option = "done";
                    break;
                case "buy":
                    break;
                default:
                    break;
            }
            channel.Send(reply);
        }
    }
}
