using Common;
using Backend.Game;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvChat(IChannel channel, Message message)
        {
            CEnemyClosing request = message as CEnemyClosing;
        }
    }
}

