using System;

namespace Common
{
    [Serializable]
    public class SChatMessage : Message
    {
        public SChatMessage() : base(Command.S_CHAT_MESSAGE) { }
        public int from;
        public int to;
        public string message;
    }
}
