using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3DGamekit.Scripts.Game
{
    class content
    {
        public int source; //0 friend, 1 myself
        public string message;
        public content(int a, string b)
        {
            source = a; 
            message = b;
        }
    }

    class World:Singleton<World>
    {
        public string selfName;
        public int selfId;
        public int selfDbid;
        public int partner;
        public Dictionary<string, int> players = new Dictionary<string, int>();
        public Dictionary<int, List<content>> chatHistory = new Dictionary<int, List<content>>();
        public Dictionary<String, int> get_players()
        {
            return players;
        }
        public void addPlayers(string name, int id)
        {
            this.players.Add(name, id);
        }
        public void init(string name, int id, int db)
        {
            this.selfName = name;
            this.selfId = id;
            this.selfDbid = db;
        }
        
    }
}
