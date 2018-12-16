using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets._3DGamekit.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvPlayerEnter(IChannel channel, Message message)
        {
            MyNetwork network = GameObject.FindObjectOfType<MyNetwork>();
            GameStart startup = GameObject.FindObjectOfType<GameStart>();
            if (network.gameScene)
            {// ignore enter scene message when debug mode
                return;
            }
            Debug.Log("Receive Enter...");
            SPlayerEnter msg = message as SPlayerEnter;
            World.Instance.init(msg.user, msg.id, msg.dbid, msg.inventory);
            World.Instance.addPlayers(msg.user, msg.id);
            foreach (KeyValuePair<string, int> kvp in World.Instance.get_players())
            {
                Debug.Log(kvp.Key);
            }
            Debug.Log(msg.id);
            startup.PlayerEnter(msg.scene);
        }
    }
}
