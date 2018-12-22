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
        private void OnRecvMyMarket(IChannel channel, Message message)
        {
            SMyMarket msg = message as SMyMarket;
            World.Instance.mymarket = msg.items;
            World.Instance.marketCount = msg.items.Count();
        }
    }
}
