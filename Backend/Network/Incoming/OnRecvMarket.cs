﻿using Common;
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
                    int gold = 0, silver = 0;
                    int silver_coin = Database.Instance.GetSilverCoins(request.dbid);
                    int gold_coin = Database.Instance.GetGoldCoins(request.dbid);
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
                    if((gold <= gold_coin && silver <= silver_coin) || request.items[0].owner_id == request.dbid)
                    {
                        if(gold > 0)
                            Database.Instance.MarketBuy(request.items, request.dbid, owner, true);
                        else
                            Database.Instance.MarketBuy(request.items, request.dbid, owner, false);
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
                    Database.Instance.MarketChange(request.items[0]);
                    reply.option = "done";
                    break;
                default:
                    break;
            }
            channel.Send(reply);
        }
    }
}
