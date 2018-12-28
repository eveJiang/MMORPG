using Common;
using UnityEngine;
using System;

namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvPlayerFlash(IChannel channel, Message message)
		{
			//public NetworkEntity Entity { get { return m_entity; } }
			//NetworkEntity m_entity;
			//m_entity = GetComponent<NetworkEntity>(); 
			Console.WriteLine(string.Format("send to fortend "));
			Debug.Log("send to fortend");
			SPlayerFlash msg = message as SPlayerFlash;
			NetworkEntity self = networkEntities[msg.ID];
			PlayerController controller = self.gameObject.GetComponent<PlayerController>();
			if (controller == null)
				return;
			IPlayerBehavior behavior = (IPlayerBehavior)(self.behavior);
			Vector3 position = new Vector3(msg.pos.x, msg.pos.y, msg.pos.z);
			Quaternion rotation = new Quaternion(msg.rot.x, msg.rot.y, msg.rot.z, msg.rot.w);
			behavior.Flash(position, rotation);
			Debug.Log("send to fortend sucess");
			Console.WriteLine(string.Format("send to fortend sucess"));

		}
	}
}
