using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CGetMessage : Message
    {
        public CGetMessage() : base(Command.C_GETMESSAGE) { }
        public int userdbid;
        public string option;
    }
}
