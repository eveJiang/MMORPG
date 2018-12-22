using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CMyMarket : Message
    {
        public CMyMarket() : base(Command.C_MYMARKET) { }
        public int dbid;
    }
}
