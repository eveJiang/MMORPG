using Common;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets._3DGamekit.Scripts.Game;

namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvName(IChannel channel, Message message)
		{
			Console.WriteLine(string.Format("send to fortend "));
			Debug.Log("send to fortend");
			SName msg = message as SName;
			NetworkEntity self = networkEntities[World.Instance.tempID];
			PlayerController controller = self.gameObject.GetComponent<PlayerController>();
			if (controller == null)
				return;
			//this.gameObject.GetComponent<Text>().text = World.Instance.selfName;
			GameObject.Find("Ellen(Clone)").GetComponent<Text>().text = World.Instance.tempname;
		}
	}
}