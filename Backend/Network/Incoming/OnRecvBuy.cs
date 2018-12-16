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
            int totalCost = 0;
            foreach (var item in request.items)
            {
                totalCost += item.price;
                //response.items.Add(item.name);
                Console.WriteLine(string.Format("item_name: {0}", item.name));
            }
            Console.WriteLine(string.Format("player_goldcoin: {0}; player_silvercoin: {1}", Database.Instance.GetGoldCoins(player.dbid), Database.Instance.GetSilverCoins(player.dbid)));
            if (totalCost <= Database.Instance.GetSilverCoins(player.dbid))
            {
                Console.WriteLine("Backend : OnRecvBuy player.dbid = {0}", player.dbid);
                Database.Instance.BuyItems(request.items, player.dbid);
                response.success = true;
            }
            response.items = Database.Instance.GetInventory(request.dbid);
            channel.Send(response);
            Console.WriteLine("Backend : Finish OnRecvBuy");
        }
    }
}
