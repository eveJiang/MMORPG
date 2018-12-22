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
        private void OnRecvCoin(IChannel channel, Message message)
        {
            SCoinMessage request = message as SCoinMessage;
            World.Instance.gold = request.gold;
            World.Instance.silver = request.silver;
        }

    }
}
