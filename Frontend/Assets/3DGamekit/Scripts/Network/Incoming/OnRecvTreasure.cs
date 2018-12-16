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
        private void OnRecvTreausre(IChannel channel, Message message)
        {
            STreasureMessage msg = message as STreasureMessage;
            World.Instance.view.effect = msg.treasure.effect;
            World.Instance.view.value = msg.treasure.value;
            World.Instance.view.price = msg.treasure.price;
            World.Instance.view.status = msg.treasure.status;
        }
    }
}
