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
                Database.Instance.ChangeStatusOn(request.userDbid, request.treasureId);
            else
                Database.Instance.ChangeStatusOff(request.userDbid, request.treasureId);
        }
    }
}

