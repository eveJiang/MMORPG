using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class STreasureMessage : Message
    {
        public STreasureMessage() : base(Command.S_TREASUREMESSAGE) { }
        public Treasure treasure = new Treasure();
    }
}