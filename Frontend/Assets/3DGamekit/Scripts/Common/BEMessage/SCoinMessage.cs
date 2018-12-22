using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SCoinMessage : Message
    {
        public SCoinMessage() : base(Command.S_COINMESSAGE) { }
        public int gold;
        public int silver;
    }
}
