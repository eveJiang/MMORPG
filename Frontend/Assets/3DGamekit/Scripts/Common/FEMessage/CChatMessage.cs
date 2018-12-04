using System;

namespace Common
{
    [Serializable]
    public class CChatMessage : Message
    {
        public CChatMessage() : base(Command.C_CHATMESSAGE) { }
        public string user;
        public string password;
    }
}
