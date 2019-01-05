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
			Console.WriteLine(string.Format("Backend recieve gold to flash"));//
			Player player = channel.GetContent() as Player;
			CFlash request = message as CFlash;
			SFlash response = new SFlash();
			response.success = false;
			response.gold = request.gold_nums;
            var conn = db.Instance.Connect();
            if (request.gold_nums <= db.Instance.GetGoldCoins(player.dbid, conn)) //身上的钱足够则可以购买
			{
				Console.WriteLine("Backend : OnRecvFlash player.dbid = {0}", player.dbid);
				db.Instance.MinesGolds(request.gold_nums, player.dbid, conn);
				response.success = true;
			}
			channel.Send(response);
		}
	}
}
