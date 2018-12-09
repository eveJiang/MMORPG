using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Assets._3DGamekit.Scripts.Game;
using UnityEngine;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            SBuyMessage msg = message as SBuyMessage;
            Debug.Log("Frontend: OnRecvBuy");
            foreach(var k in msg.items)
            {
                World.Instance.addItem(k);
                World.Instance.inventoryCount++;
                Debug.Log(string.Format("Frontend: OnRecvBuy {0} Item", k));
            }
        }

    }
}