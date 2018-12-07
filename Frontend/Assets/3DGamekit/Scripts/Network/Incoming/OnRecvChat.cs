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
        private void OnRecvChat(IChannel channel, Message message)
        {
            SChatMessage msg = message as SChatMessage;
            if (World.Instance.chatHistory.ContainsKey(msg.from))
            {
                World.Instance.chatHistory[msg.from].Add(new content(0, msg.message));
                Console.WriteLine(string.Format("Front: On receive chat. from : {0}, to {1}, message {2}", msg.from, msg.to, msg.message));
            }
            else
            {
                Console.WriteLine(string.Format("Initial Front: On receive chat. from : {0}, to {1}, message {2}", msg.from, msg.to, msg.message));
                List<content> temp = new List<content>();
                World.Instance.chatHistory.Add(msg.from, temp);
                World.Instance.chatHistory[msg.from].Add(new content(0, msg.message));
            }
        }

    }
}
