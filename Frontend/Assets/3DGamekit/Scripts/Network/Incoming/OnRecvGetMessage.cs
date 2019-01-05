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
        private void OnRecvGetMessage(IChannel channel, Message message)
        {
            SGetMessage msg = message as SGetMessage;
            World.Instance.myinventory = msg.invent;
        }

    }
}