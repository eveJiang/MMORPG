using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Assets._3DGamekit.Scripts.Game;
using UnityEngine;

namespace Gamekit3D.Network
{
	public partial class Incoming : MonoBehaviour
	{
		//public GameObject close_UI;
		private void OnRecvFlash(IChannel channel, Message message)
		{
			SFlash msg = message as SFlash;
			Debug.Log("Frontend: OnRecvFlash");
			if (msg.success == true)
			{
				MessageBox.Show(string.Format("Successfully deduct {0} golds. Start teleporatation!",msg.gold));
				GameObject.Find("Ellen(Clone)").GetComponent<PlayerMyController>().SendFlash(1, 8, 120);
				MessageBox.Show("Enjoy your trip! You have to find the plate to leave.");
				//close_UI.SetActive(false);
			}
			else
				MessageBox.Show("You don't have enough money ; (");
		}

	}
}