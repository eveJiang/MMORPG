﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SMarketMessage : Message
    {
        public SMarketMessage() : base(Command.S_MARKETMESSAGE) { }
        public List<MarketTreasure> items = new List<MarketTreasure>();
        public List<Treasure> invent = new List<Treasure>();
        public string option;
        public bool success;
        public int silver;
        public int gold;
    }
}
