using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
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
        public List<Treasure> myinventory = new List<Treasure>();
        public Dictionary<String, Treasure> defence = new Dictionary<String, Treasure>();
        public Dictionary<String, Treasure> attack = new Dictionary<String, Treasure>();
        public Dictionary<String, Treasure> speed = new Dictionary<string, Treasure>();
        public Dictionary<String, Treasure> intelligence = new Dictionary<string, Treasure>();
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
        public void init(string name, int id, int db, List<Treasure> items)
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
        public void setView(Treasure a)
        {
            view.name = a.name;
            view.id = a.id;
            view.value = a.value;
            view.price = a.price;
            view.type = a.type;
            view.effect = a.effect;
            view.status = a.status;
        }
        public void addItem(Treasure newItem)
        {
            myinventory.Add(newItem);
        }
        public void removeItem(Treasure newItem)
        {
            myinventory.Remove(newItem);
        }
        public bool check()
        {
            int k = myinventory.IndexOf(view);
            if (view.effect == '0' && defence.Count < 2 && defence.ContainsValue(view) == false)
            {
                defence.Add(view.name, view);
                return true;
            }
            else if (view.effect == '1')
            {
                life += view.value;
                removeItem(view);
                return true;
            }
            else if (view.effect == '2' && intelligence.Count < 2 && intelligence.ContainsValue(view) == false)
            {
                intelligence.Add(view.name, view);
                return true;
            }
            else if (view.effect == '3' && speed.Count < 2 && speed.ContainsValue(view) == false)
            {
                speed.Add(view.name, view);
                return true;
            }
            else if (view.effect == '4' && attack.Count < 2 && attack.ContainsValue(view) == false)
            {
                attack.Add(view.name, view);
                return true;
            }
            else return false;
        }
        public bool off()
        {
            int k = myinventory.IndexOf(view);
            if (view.effect == '0'&& defence.ContainsValue(view) == true)
            {
                defence.Remove(view.name);
                return true;
            }
            else if (view.effect == '2' && intelligence.ContainsValue(view) == true)
            {
                intelligence.Remove(view.name);
                return true;
            }
            else if (view.effect == '3' && speed.ContainsValue(view) == true)
            {
                speed.Remove(view.name);
                return true;
            }
            else if (view.effect == '4' && attack.ContainsValue(view) == true)
            {
                attack.Remove(view.name);
                return true;
            }
            else return false;
        }
    }
}
