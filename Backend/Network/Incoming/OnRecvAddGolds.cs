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
		private void OnRecvAddGolds(IChannel channel, Message message)
		{
			CAddGolds msg = message as CAddGolds;
            var conn = db.Instance.Connect();
            db.Instance.AddGolds(msg.gold_nums,msg.dbid, conn);
			Console.WriteLine(string.Format("add nums = {0}", msg.gold_nums));
		}
	}
}
