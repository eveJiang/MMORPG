using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CBuyMessage : Message
    {
        public CBuyMessage() : base(Command.C_BUYMESSAGE) { }
        public List<Treasure> items = new List<Treasure>();
    }
}
