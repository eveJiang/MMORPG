﻿using System;

namespace Common
{
    [Serializable]
    public class CChatMessage : Message
    {
        public CChatMessage() : base(Command.C_CHAT_MESSAGE) { }
        public int from;
        public int to;
        public string message;
    }
}
