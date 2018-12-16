using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CTreasureMessage: Message
    {
        public CTreasureMessage() : base(Command.C_TREASUREMESSAGE) { }
        public int dbid;
        public string treasureName;
    }
}
