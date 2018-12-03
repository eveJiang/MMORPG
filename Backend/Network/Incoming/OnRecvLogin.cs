using Common;
using Backend.Game;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvLogin(IChannel channel, Message message)
        {
            CLogin request = message as CLogin;
            SPlayerEnter response = new SPlayerEnter();
            string scene = "Level1";
            response.user = request.user;
            response.token = request.user;
            response.scene = scene;
            
            var db = Backend.Database.Instance;

            if (db.LoginUser(request.user.ToString(), request.password.ToString()))
            {
                ClientTipInfo(channel, "Successfully login :)");
                channel.Send(response);
            }
            else
            {
                ClientTipInfo(channel, "Sorry :(");
            }
            //ClientTipInfo(channel, string.Format("('{0}', '{1}');", request.user.ToString(), request.password.ToString()));

            Player player = new Player(channel);
            player.scene = scene;
            // TODO read from database
            DEntity dentity = World.Instance.EntityData["Ellen"];
            player.FromDEntity(dentity);
            player.forClone = false;
            //ClientTipInfo(channel, "TODO: get player's attribute from database");
            // player will be added to scene when receive client's CEnterSceneDone message
        }
    }
}
