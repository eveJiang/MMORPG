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
        private void OnRecvCommunity(IChannel channel, Message message)
        {
            Debug.Log("ccccccomunity");
            SCommunity msg = message as SCommunity;
            if(World.Instance.players.ContainsKey(msg.name) == false && msg.enter == true)
            {
                Debug.Log("ENTER");
                World.Instance.addPlayers(msg.name, msg.id);
            }
            else if(World.Instance.players.ContainsKey(msg.name) == true && msg.enter == false)
            {
                World.Instance.players.Remove(msg.name);
            }
        }

    }
}
