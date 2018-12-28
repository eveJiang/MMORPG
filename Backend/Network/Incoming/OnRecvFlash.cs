using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
	public partial class Incoming
	{
		private void OnRecvFlash(IChannel channel, Message message)
		{
			Player player = channel.GetContent() as Player;
			CFlash request = message as CFlash;
			SFlash response = new SFlash();
			response.success = false;
			response.gold = request.gold_nums;
			if (request.gold_nums <= Database.Instance.GetSilverCoins(player.dbid)) //身上的钱足够则可以购买
			{
				Console.WriteLine("Backend : OnRecvFlash player.dbid = {0}", player.dbid);
				Database.Instance.FlashCost(request.gold_nums, player.dbid);
				response.success = true;
			}
			channel.Send(response);
		}
	}
}
