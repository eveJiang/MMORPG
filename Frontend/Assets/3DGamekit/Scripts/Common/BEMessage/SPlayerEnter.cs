using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SPlayerEnter : Message
    {
        public SPlayerEnter() : base(Command.S_PLAYER_ENTER) { }
        public string user;
        public string token;
        public string scene;
        public int id;
        public string name;
        public int dbid;
        public int silver;
        public int gold;
        public int teammate_id;
        public List<Treasure> inventory;
        public List<MarketTreasure> market;
    }
}
