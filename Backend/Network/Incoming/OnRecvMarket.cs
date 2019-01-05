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
            var conn = db.Instance.Connect();
            switch (request.option)
            {
                case "get": 
                    reply.items = db.Instance.GetMarket(conn);
                    reply.option = "get";
                    break;
                case "sell":
                    db.Instance.MarketSell(request.items[0], conn);
                    reply.option = "done";
                    break;
                case "buy":
                    int gold = 0, silver = 0;
                    //int silver_coin = db.Instance.GetSilverCoins(request.dbid, conn);
                    //int gold_coin = db.Instance.GetGoldCoins(request.dbid, conn);
                    foreach (var i in request.items)
                    {
                        if (i.coinType)
                            gold += i.price;
                        else silver += i.price;
                        Treasure temp = new Treasure();
                        temp.id = i.id;
                        temp.name = i.name;
                        temp.value = i.value;
                        temp.price = i.price;
                        temp.type = i.type;
                        temp.effect = i.effect;
                        temp.status = i.status;
                        reply.invent.Add(temp);
                    }
                    int owner = request.items[0].owner_id;
                    if(/*(gold <= gold_coin && silver <= silver_coin) || request.items[0].owner_id == request.dbid*/ true)
                    {
                        if(gold > 0)
                            db.Instance.MarketBuy(request.items, request.dbid, owner, true, conn);
                        else
                            db.Instance.MarketBuy(request.items, request.dbid, owner, false, conn);
                        reply.success = true;
                        reply.gold = gold;
                        reply.silver = silver;
                    }
                    else
                    {
                        reply.success = false;
                    }
                    reply.option = "buy";
                    break;
                case "change":
                    db.Instance.MarketChange(request.items[0], conn);
                    reply.option = "done";
                    break;
                default:
                    break;
            }
            channel.Send(reply);
        }
    }
}
