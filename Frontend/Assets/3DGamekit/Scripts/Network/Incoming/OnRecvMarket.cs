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
                default:
                    break;
            }
        }
    }
}
