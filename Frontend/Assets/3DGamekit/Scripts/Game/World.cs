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
        public List<String> myinventory = new List<String>();
        public int inventoryCount = 0;
        public int inventoryCapacity = 40;
        public Dictionary<String, int> get_players()
        {
            return players;
        }
        public void addPlayers(string name, int id)
        {
            this.players.Add(name, id);
        }
        public void init(string name, int id, int db, List<String> items)
        {
            this.selfName = name;
            this.selfId = id;
            this.selfDbid = db;
            foreach(var item in items)
            {
                myinventory.Add(item);
                inventoryCount++;
            }
        }
        public void addItem(String name)
        {
            myinventory.Add(name);
        }

    }
}
