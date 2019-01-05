using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SGetMessage : Message
    {
        public SGetMessage() : base(Command.S_GETMESSAGE) { }
        public List<MarketTreasure> items = new List<MarketTreasure>();
        public List<Treasure> invent = new List<Treasure>();
        public string option;
    }
}
