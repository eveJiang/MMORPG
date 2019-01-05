using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvChangeStatus(IChannel channel, Message message)
        {
            CChangeStatus request = message as CChangeStatus;
            var conn = db.Instance.Connect();
            if (request.on == true)
            {
                if (request.treasure.effect != '1')
                    db.Instance.ChangeStatusOn(request.userDbid, request.treasure.id, conn);
                else
                    db.Instance.DeleteTreasure(request.treasure.id, conn);
            }
            else
                db.Instance.ChangeStatusOff(request.userDbid, request.treasure.id, conn);
        }
    }
}

