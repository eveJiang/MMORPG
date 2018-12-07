using System;

namespace Common
{
    [Serializable]
    public class SCommunity : Message
    {
        public SCommunity() : base(Command.S_COMMUNITY) { }
        public string name;
        public int id;
        public bool enter; 
    }
}
