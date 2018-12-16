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
        public struct Treasure
        {
            public int id;
            public string name;
            public int value;
            public int price;
            public char type;
            public char effect;
            public char status;
        }

        public string selfName;
        public int selfId;
        public int selfDbid;
        public int partner;
        public Dictionary<string, int> players = new Dictionary<string, int>();
        public Dictionary<int, List<content>> chatHistory = new Dictionary<int, List<content>>();
        public List<String> myinventory = new List<String>();
        public Dictionary<String, int> defence = new Dictionary<String, int>();
        public Dictionary<String, int> attack = new Dictionary<String, int>();
        public Dictionary<String, int> speed = new Dictionary<string, int>();
        public Dictionary<String, int> intelligence = new Dictionary<string, int>();
        public int life;
        public int inventoryCount = 0;
        public int inventoryCapacity = 40;
        public Treasure view = new Treasure(); //正在看的商品
        public int messageLock = 1;
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
        public void removeItem(String name)
        {
            myinventory.Remove(name);
        }
        public bool check()
        {
            if (view.effect == '0' && defence.Count < 2 && defence.ContainsKey(view.name) == false)
            {
                defence.Add(view.name, view.value);
                return true;
            }
            else if (view.effect == '1')
            {
                life += view.value;
                removeItem(view.name);
                return true;
            }
            else if (view.effect == '2' && intelligence.Count < 2 && intelligence.ContainsKey(view.name) == false)
            {
                intelligence.Add(view.name, view.value);
                return true;
            }
            else if (view.effect == '3' && speed.Count < 2 && speed.ContainsKey(view.name) == false)
            {
                speed.Add(view.name, view.value);
                return true;
            }
            else if (view.effect == '4' && attack.Count < 2 && attack.ContainsKey(view.name) == false)
            {
                attack.Add(view.name, view.value);
                return true;
            }
            else return false;
        }
    }
}
