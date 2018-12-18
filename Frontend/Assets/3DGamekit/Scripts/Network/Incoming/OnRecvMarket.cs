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
                    if (msg.success == true)
                    {
                        foreach (var k in msg.items)
                        {
                            Treasure t = new Treasure();
                            t.id = k.id;
                            t.name = k.name;
                            t.value = k.value;
                            t.type = k.type;
                            t.effect = k.effect;
                            t.status = k.status;
                            if (World.Instance.myinventory.Contains(t) == false)
                            {
                                World.Instance.addItem(t);
                                World.Instance.inventoryCount++;
                                Debug.Log(string.Format("Frontend: OnRecvBuy {0} Item", k));
                            }
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
