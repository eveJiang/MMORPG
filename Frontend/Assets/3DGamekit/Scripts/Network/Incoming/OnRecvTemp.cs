using Common;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets._3DGamekit.Scripts.Game;

namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvTemp(IChannel channel, Message message)
		{
			STemp msg = message as STemp;
			World.Instance.tempID = msg.entityid;
			World.Instance.tempname = msg.name;
		}
	}
}
