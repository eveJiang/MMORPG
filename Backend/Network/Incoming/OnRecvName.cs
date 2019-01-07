using Common;
using Backend.Game;
using System;
using UnityEngine;

namespace Backend.Network
{
	public partial class Incoming
	{
		private void OnRecvName(IChannel channel, Message message)
		{
			Console.WriteLine(string.Format("Name send to backend start"));
			CName request = message as CName;
			//Player player = (Player)World.Instance.GetEntity(request.entityid);

			SName response = new SName();
			//response.entityid = request.entityid;
			//response.self = request.self;//不能传到后端

			//channel.Send(response);
			//player.Broadcast(response, true);
			World.Instance.Broundcast(response);
			Console.WriteLine(string.Format("send to backend sucess"));


		}
	}
}