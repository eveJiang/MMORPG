using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
	public partial class Incoming
	{
		private void OnRecvPlayerFlash(IChannel channel, Message message)
		{
			Console.WriteLine(string.Format("send to backend start"));
			CPlayerFlash request = message as CPlayerFlash;
			Player player = (Player)World.Instance.GetEntity(request.player);

			player.Position = Entity.V3ToPoint3d(request.pos);
			SPlayerFlash response = new SPlayerFlash();
			response.ID = request.player;
			response.pos = request.pos;
			response.rot = request.rot;

			//channel.Send(response);
			//player.Broadcast(response, true);
			World.Instance.Broundcast(response);
			Console.WriteLine(string.Format("send to backend sucess"));


		}
	}
}