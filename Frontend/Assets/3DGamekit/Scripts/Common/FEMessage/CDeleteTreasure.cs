using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CDeleteTreasure : Message
    {
        public CDeleteTreasure() : base(Command.C_DELETETREASURE) { }
        public int treasureId;
    }
}
