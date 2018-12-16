using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SBuyMessage : Message
    {
        public SBuyMessage() : base(Command.S_BUYMESSAGE) { }
        public List<Treasure> items = new List<Treasure>();
        public bool success;
    }
}
