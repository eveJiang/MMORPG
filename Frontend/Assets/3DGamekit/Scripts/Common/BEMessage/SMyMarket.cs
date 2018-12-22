using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SMyMarket : Message
    {
        public SMyMarket() : base(Command.S_MYMARKET) { items = new List<MarketTreasure>(); }
        public List<MarketTreasure> items = new List<MarketTreasure>();
    }
}
