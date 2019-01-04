using System;
using Common;

namespace Backend.Network
{
	public partial class Incoming
	{
		private void OnRecvMinesGolds(IChannel channel, Message message)
		{
			CMinesGolds msg = message as CMinesGolds;
			//Database.Instance.AddGolds(msg.gold_nums, msg.dbid);
			int nums = Database.Instance.GetGoldCoins(msg.dbid);
			if (msg.gold_nums >= nums)//身上的钱足够
			{
				Database.Instance.MinesGolds(msg.gold_nums, msg.dbid);
				Console.WriteLine(string.Format("Player {0} Mines nums = {1}", msg.dbid, msg.gold_nums));
			}
			else
			{
				Database.Instance.MinesGolds(nums, msg.dbid);
				Console.WriteLine(string.Format("Player {0} Mines nums = {1}", msg.dbid, nums));
			}
			
		}
	}
}
