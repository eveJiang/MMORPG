using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CBuyMessage : Message
    {
        public CBuyMessage() : base(Command.C_) { }
        public string user;
        public string password;
    }
}
