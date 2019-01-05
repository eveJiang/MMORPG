using Common;
using Backend.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvGetMessage(IChannel channel, Message message)
        {
            CGetMessage request = message as CGetMessage;
            SGetMessage response = new SGetMessage();
            var conn = db.Instance.Connect();
            switch (request.option)
            {
                case "inventory":
                    response.invent = db.Instance.GetInventory(request.userdbid, conn);
                    break;
                case "market":
                    response.items = db.Instance.GetMyMarket(request.userdbid, conn);
                    break;
            }
            response.option = request.option;
            channel.Send(response);
            Console.WriteLine("Backend : Finish OnRecvBuy");
        }
    }
}
