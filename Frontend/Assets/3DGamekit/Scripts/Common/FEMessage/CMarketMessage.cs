using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CMarketMessage : Message
    {
        public CMarketMessage() : base(Command.C_MARKETMESSAGE) { items = new List<MarketTreasure>(); }
        public string option;
        public List<MarketTreasure> items;
        public int dbid;
    }
}
