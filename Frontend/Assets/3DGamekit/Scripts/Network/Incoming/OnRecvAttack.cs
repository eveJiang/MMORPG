using Common;
using Assets._3DGamekit.Scripts.Game;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvAttack(IChannel channel, Message message)
        {
            SAttack msg = message as SAttack;
            NetworkEntity source = networkEntities[msg.ID];
            if (msg.targetID != 0)
            {
                NetworkEntity target = networkEntities[msg.targetID];
                source.behavior.Attack(target.behavior);
                if (msg.targetID == World.Instance.selfId && source.entityType == EntityType.PLAYER)
                    World.Instance._deathId = source.entityId;
                else
                    World.Instance._deathId = -1;
            }
            else
            {
                source.behavior.Attack(null);
            }
        }
    }
}
