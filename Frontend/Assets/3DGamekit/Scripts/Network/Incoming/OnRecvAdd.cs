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
        private void OnRecvAdd(IChannel channel, Message message)
        {
            SAddFriend msg = message as SAddFriend;
            switch (msg.option)
            {
                case "get":
                    World.Instance.myAddFriend = msg.items;
                    break;
                default:
                    break;
            }
        }
    }
}
