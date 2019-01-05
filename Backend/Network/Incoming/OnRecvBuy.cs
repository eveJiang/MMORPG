using Common;
using Backend.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            Player player = channel.GetContent() as Player;
            CBuyMessage request = message as CBuyMessage;
            SBuyMessage response = new SBuyMessage();
            response.success = false;
            Console.WriteLine("Backend: OnRecvBuy.cs");
            var conn = db.Instance.Connect();
            int totalCost = 0;
            int mark = 0;
            int silver = 0;
            int gold = 0;
            foreach (var item in request.items)
            {
                totalCost += item.price;
                if (item.type != 'e')
                {
                    mark = 1;
                    gold += item.price;
                }
                else silver += item.price;
                Console.WriteLine(string.Format("item_name: {0}", item.name));
            }
            response.gold = gold;
            response.silver = silver;
            Console.WriteLine(string.Format("player_goldcoin: {0}; player_silvercoin: {1}", Database.Instance.GetGoldCoins(player.dbid), Database.Instance.GetSilverCoins(player.dbid)));
            if (db.Instance.BuyItems(request.items, gold, silver, player.dbid, mark, conn))
            {
                response.success = true;
            }
            response.items = db.Instance.GetInventory(request.dbid, conn);
            channel.Send(response);
            Console.WriteLine("Backend : Finish OnRecvBuy");
        }
    }
}
