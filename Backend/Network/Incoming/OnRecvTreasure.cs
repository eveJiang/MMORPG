using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvTreasure(IChannel channel, Message message)
        {
            Console.WriteLine("Backend: OnRecvTreasure, Receive Message");
            CTreasureMessage request = message as CTreasureMessage;
            STreasureMessage reply = new STreasureMessage();
            var conn = db.Instance.Connect();
            reply.treasure = db.Instance.GetTreasure(Convert.ToInt16(request.dbid), Convert.ToString(request.treasureName), conn);
            channel.Send(reply);
            //Console.WriteLine(string.Format("Backend: OnRecvTreasure {0}, {1}， {2}, {3}", reply.treasure.name, reply.treasure.type, reply.treasure.value, reply.treasure.effect));
        }
    }
}
