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
        private void OnRecvMarket(IChannel channel, Message message)
        {
            SMarketMessage msg = message as SMarketMessage;
            switch (msg.option)
            {
                case "get":
                    Client.Instance.market = msg.items;
                    break;
                case "buy":
                    Debug.Log("OnRecvMark : Buy");
                    if (msg.success == true)
                    {
                        Debug.Log("OnRecvMark : Success");
                        foreach (var k in msg.invent)
                        {
                            World.Instance.addItem(k);
                            World.Instance.inventoryCount++;
                            Debug.Log(string.Format("Frontend: OnRecvMarket {0} Item", k));
                        }
                        MessageBox.Show(":)");
                        World.Instance.silver -= msg.silver;
                        World.Instance.gold -= msg.gold;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
