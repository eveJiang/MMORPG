using Common;
using Backend.Game;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvChat(IChannel channel, Message message)
        {
            CChatMessage request = message as CChatMessage;
            SChatMessage reply = new SChatMessage();
            reply.message = request.message;
            reply.from = request.from;
            reply.to = request.to;
            Player player = World.Instance.GetEntity(reply.to) as Player;
            Console.WriteLine(string.Format("Back: On receive chat. from : {0}, to {1}, message {2}", reply.from, reply.to, reply.message));
            player.connection.Send(reply);

        }
    }
}

