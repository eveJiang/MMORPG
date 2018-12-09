using Common;
using System;
using Backend.Game;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvDamage(IChannel channel, Message message)
        {
            CDamage request = message as CDamage;
            Creature creature = World.Instance.GetEntity(request.entityId) as Creature;
            if (creature == null)
                return;
            Console.WriteLine(request.entityId);
            creature.OnHit(null, request.decHP);
        }
    }
}
