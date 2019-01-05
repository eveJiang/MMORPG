using Common;
using Backend.Game;
using System;
using System.IO;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvRegister(IChannel channel, Message message)
        {
            CRegister request = message as CRegister;
            var userName = request.user;
            var userPassword = request.password;
            var conn = db.Instance.Connect();
            Random ran = new Random();
            int k = db.Instance.RegisterUser(userName, userPassword, conn);
            Console.WriteLine(k.ToString());
            ClientTipInfo(channel, k.ToString());
            if (k == 2)
            {
                ClientTipInfo(channel, string.Format("Name {0} have already been used :(", userName));
            }
            else if (k == 0)
            {
                Console.WriteLine("system error");
                ClientTipInfo(channel, "System error :(");
            }
            else
            {
                Console.WriteLine("system success");
                ClientTipInfo(channel, "Congratulations :)");
            }
            
        }
    }
}
