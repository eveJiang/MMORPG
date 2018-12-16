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
            if (request.on == true)
            {
                if (request.treasure.effect != '1')
                    Database.Instance.ChangeStatusOn(request.userDbid, request.treasure.id);
                else
                    Database.Instance.DeleteTreasure(request.treasure.id);
            }
            else
                Database.Instance.ChangeStatusOff(request.userDbid, request.treasure.id);
        }
    }
}

