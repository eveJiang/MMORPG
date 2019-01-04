using System;
using System.Collections.Generic;
using Backend.Game;
using Common;
using System.Collections;

namespace Backend.Network
{
	public partial class Incoming
	{
		private void OnRecvMinesGolds(IChannel channel, Message message)
		{
			CMinesGolds msg = message as CMinesGolds;
			//Database.Instance.AddGolds(msg.gold_nums, msg.dbid);
			int nums = Database.Instance.GetGoldCoins(msg.Sdbid);
			int Adbid = -1;
			//List<int> Players = ;
			Player player = new Player(channel);
			Scene scenes = World.Instance.GetScene(player.scene);
			foreach (var tmp in scenes.Players)
			{
				if(tmp.Value.entityId == msg.AentityId)
				{
					Adbid = tmp.Value.dbid;
					break;
				}
			}
			if (msg.gold_nums >= nums)//身上的钱足够
			{
				Database.Instance.MinesGolds(msg.gold_nums, msg.Sdbid);
				Console.WriteLine(string.Format("Player {0} Mines nums = {1}", msg.Sdbid, msg.gold_nums));
				if(Adbid != -1)
					Database.Instance.AddGolds(msg.gold_nums, Adbid);
				else
				{
					Console.WriteLine(string.Format("Error to find Attacker!!!!"));
				}
			}
			else
			{
				Database.Instance.MinesGolds(nums, msg.Sdbid);
				Console.WriteLine(string.Format("Player {0} Mines nums = {1}", msg.Sdbid, nums));
				if (Adbid != -1)
					Database.Instance.AddGolds(nums, Adbid);
				else
				{
					Console.WriteLine(string.Format("Error to find Attacker!!!!"));
				}
			}
			
		}
	}
}
