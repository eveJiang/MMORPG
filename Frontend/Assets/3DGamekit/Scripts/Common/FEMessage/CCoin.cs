using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CCoinMessage : Message
    {
        public CCoinMessage() : base(Command.C_COINMESSAGE) { }
        public int userdbid;
    }
}
