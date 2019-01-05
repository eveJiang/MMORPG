using Common;
using UnityEngine;
using Assets._3DGamekit.Scripts.Game;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvAttack(IChannel channel, Message message)
        {
            SAttack msg = message as SAttack;
            NetworkEntity source = networkEntities[msg.ID];
            if (msg.targetID != 0 && msg.targetID != World.Instance.teammate_dbid)
            {
                NetworkEntity target = networkEntities[msg.targetID];
                source.behavior.Attack(target.behavior);
            }
            else
            {
                source.behavior.Attack(null);
            }
        }
    }
}
