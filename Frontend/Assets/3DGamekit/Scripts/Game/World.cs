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

    class World : Singleton<World>
    {
        public string selfName;
        public int selfId;
        public int selfDbid;
        public int partner;
        public Dictionary<string, int> players = new Dictionary<string, int>();
        public Dictionary<int, List<content>> chatHistory = new Dictionary<int, List<content>>();
        public List<Treasure> myinventory = new List<Treasure>();
        public List<Treasure> mymarket = new List<Treasure>();
        public Dictionary<String, Treasure> defence = new Dictionary<String, Treasure>();
        public Dictionary<String, Treasure> attack = new Dictionary<String, Treasure>();
        public Dictionary<String, Treasure> speed = new Dictionary<String, Treasure>();
        public Dictionary<String, Treasure> intelligence = new Dictionary<String, Treasure>();
        public Dictionary<int, String> position = new Dictionary<int, String>(); //dbid, position
        public Dictionary<String, int> occupied = new Dictionary<String, int>(); //0 空闲 1 占有
        public int count_HP = 5;
        public int count_defence = 0;
        public int count_attack = 0;
        public int count_speed = 0;
        public int count_intelligence = 0;
        public int inventoryCount = 0;
        public int inventoryCapacity = 40;
        public Treasure view = new Treasure(); //正在看的商品
        public int messageLock = 1;
        public int gold;
        public int silver;
        public Dictionary<String, int> get_players()
        {
            return players;
        }
        public void addPlayers(string name, int id)
        {
            this.players.Add(name, id);
        }
        public void init(string name, int id, int db, List<Treasure> items, int gcoin, int scoin)
        {
            this.selfName = name;
            this.selfId = id;
            this.selfDbid = db;
            this.gold = gcoin;
            this.silver = scoin;
            foreach (var item in items)
            {
                myinventory.Add(item);
                inventoryCount++;
            }
            occupied.Add("Defence1", 0);
            occupied.Add("Defence2", 0);
            occupied.Add("Speed1", 0);
            occupied.Add("Speed2", 0);
            occupied.Add("Intelligence1", 0);
            occupied.Add("Intelligence2", 0);
            occupied.Add("Attack1", 0);
            occupied.Add("Attack2", 0);
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
                count_defence += view.value;
                if (occupied["Defence1"] == 0)
                {
                    occupied["Defence1"] = 1;
                    position.Add(view.id, "Defence1");
                }
                else
                {
                    occupied["Defence2"] = 1;
                    position.Add(view.id, "Defence2");
                }
                myinventory.Remove(view);
                view.status = '2';
                myinventory.Add(view);
                defence.Add(view.name, view);
                return true;
            }
            else if (view.effect == '1' && view.status == '1')
            {
                count_HP += view.value;
                removeItem(view);
                view.status = '2';
                return true;
            }
            else if (view.effect == '2' && intelligence.Count < 2 && intelligence.ContainsValue(view) == false)
            {
                count_intelligence += view.value;
                if (occupied["Intelligence1"] == 0)
                {
                    occupied["Intelligence1"] = 1;
                    position.Add(view.id, "Intelligence1");
                }
                else
                {
                    occupied["Intelligence2"] = 1;
                    position.Add(view.id, "Intelligence2");
                }
                myinventory.Remove(view);
                view.status = '2';
                myinventory.Add(view);
                intelligence.Add(view.name, view);
                return true;
            }
            else if (view.effect == '3' && speed.Count < 2 && speed.ContainsValue(view) == false)
            {
                count_speed += view.value;
                if (occupied["Speed1"] == 0)
                {
                    occupied["Speed1"] = 1;
                    position.Add(view.id, "Speed1");
                }
                else
                {
                    occupied["Speed2"] = 1;
                    position.Add(view.id, "Speed2");
                }
                myinventory.Remove(view);
                view.status = '2';
                myinventory.Add(view);
                speed.Add(view.name, view);
                return true;
            }
            else if (view.effect == '4' && attack.Count < 2 && attack.ContainsValue(view) == false)
            {
                count_attack += view.value;
                if (occupied["Attack1"] == 0)
                {
                    occupied["Attack1"] = 1;
                    position.Add(view.id, "Attack1");
                }
                else
                {
                    occupied["Attack2"] = 1;
                    position.Add(view.id, "Attack2");
                }
                myinventory.Remove(view);
                view.status = '2';
                myinventory.Add(view);
                attack.Add(view.name, view);
                return true;
            }
            else return false;
        }
        public bool off()
        {
            int k = myinventory.IndexOf(view);
            if (view.effect == '0' && defence.ContainsValue(view) == true)
            {
                count_defence -= view.value;
                defence.Remove(view.name);
            }
            else if (view.effect == '2' && intelligence.ContainsValue(view) == true)
            {
                count_intelligence -= view.value;
                intelligence.Remove(view.name);
            }
            else if (view.effect == '3' && speed.ContainsValue(view) == true)
            {
                count_speed -= view.value;
                speed.Remove(view.name);
            }
            else if (view.effect == '4' && attack.ContainsValue(view) == true)
            {
                count_attack -= view.value;
                attack.Remove(view.name);
            }
            else return false;
            occupied[position[view.id]] = 0;
            myinventory.Remove(view);
            view.status = '1';
            myinventory.Add(view);
            return true;
        }
    }
}
