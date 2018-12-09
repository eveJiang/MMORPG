using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            Player player = channel.GetContent() as Player;
            CBuyMessage request = message as CBuyMessage;
            Console.WriteLine("Backend: OnRecvBuy.cs");
            int totalCost = 0;
            foreach (var item in request.items)
            {
                totalCost += item.price;
                Console.WriteLine(string.Format("item_name: {0}", item.name));
            }
            Console.WriteLine(string.Format("player_goldcoin: {0}; player_silvercoin: {1}", Database.Instance.GetGoldCoins(player.dbid), Database.Instance.GetSilverCoins(player.dbid)));
            if (totalCost <= Database.Instance.GetSilverCoins(player.dbid))
                Database.Instance.BuyItems(request.items, player.dbid);
        }
    }
}
