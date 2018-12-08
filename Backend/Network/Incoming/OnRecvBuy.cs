using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            CBuyMessage request = message as CBuyMessage;
            int totalCost = 0;
            foreach (var item in request.items)
            {
                totalCost += item.price;
            }
            if (totalCost <= Database.Instance.GetSilverCoins())
                Database.Instance.BuyItems(request.items);
        }
    }
}
