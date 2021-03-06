﻿using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvDelete(IChannel channel, Message message)
        {
            CDeleteTreasure request = message as CDeleteTreasure;
            var conn = db.Instance.Connect();
            db.Instance.DeleteTreasure(request.treasureId, conn);
        }
    }
}

