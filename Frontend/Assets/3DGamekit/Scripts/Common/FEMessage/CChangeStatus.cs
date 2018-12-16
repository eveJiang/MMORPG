using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CChangeStatus : Message
    {
        public CChangeStatus() : base(Command.C_STATUSMESSAGE) { }
        public int userDbid;
        public int treasureId;
        public bool on; //true 穿衣服 false 脱衣服
    }
}
