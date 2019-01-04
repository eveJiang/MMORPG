using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Common;
using Backend.Game;

namespace Backend
{
    class Database : Singleton<Database>
    {
        private NpgsqlConnection conn = null;

        public bool Connect(string ip="219.228.148.179", int port=5432)
        {

            var connString = string.Format("Host=" + ip + ";Port={0:D4};Username=postgres;Password=0515;Database=game", port);
            conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw (e);
            }
            return true;
        }

        public int RegisterUser(string username, string password)
        {
            int count = 0;
            var cmd = new NpgsqlCommand(string.Format("select * from player where name = '{0}';", username), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            if (count != 0)
                return 2;   //user name error
            var cmd2 = new NpgsqlCommand(string.Format("insert into \"player\"(name, password, gold_coin, silver_coin) values('{0}', '{1}', 20, 100);", username, password), conn);
            var ret = cmd2.ExecuteNonQuery();
            if (ret != 0)
                return 1;  //success
            else return 0; //fail
        }

        public bool LoginUser(string username, string password)
        {
            int count = 0;
            var cmd = new NpgsqlCommand(string.Format("select * from player where name = '{0}' and password = '{1}';", username, password), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            if (count != 0)
                return true;   //success
            else
                return false;

        }

        public int GetID(string username)
        {
            var cmd = new NpgsqlCommand(string.Format("select id from player where name = '{0}';", username), conn);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public int GetSilverCoins(int id)
        {
            var cmd = new NpgsqlCommand(string.Format("select silver_coin from player where id = '{0:D}';", id), conn);
            int coins = Convert.ToInt32(cmd.ExecuteScalar());
            return coins;
        }

        public int GetGoldCoins(int id)
        {
            using (NpgsqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    var cmd = new NpgsqlCommand(string.Format("select gold_coin from player where id = '{0:D}';", id), conn);
                    int coins = Convert.ToInt32(cmd.ExecuteScalar());
                    return coins;
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool BuyItems(List<Treasure> items, int id, int mark)
        {
            int sum = 0;
            int gold = 0;
            int silver = 0;
            if(mark == 1) //一旦有别的东西加入进来，就需要transaction了，知道了嘛
            {
                NpgsqlTransaction tr = (NpgsqlTransaction)conn.BeginTransaction();
                foreach (var item in items)
                {
                    //status: 1 occupied; 2 dressing; 3 sale
                    var cmd = new NpgsqlCommand(string.Format("insert into \"treasure\"(name, type, effect, value, price, status, owner_id) values('{0}','{1}','{2}',{3},{4},'{5}',{6});",
                                                                item.name, item.type, item.effect, item.value, item.price, '1', id), conn);
                    Console.WriteLine(string.Format("insert into \"treasure\"(name, value, price, status, owner_id) values('{0}',{1},{2},'{3}',{4});",
                                                        item.name, item.value, item.price, '1', id));
                    cmd.Transaction = tr;
                    cmd.ExecuteScalar();
                    if (item.type == 'e') silver += item.price;
                    else gold += item.price;
                }
                var cmd2 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id={1};", silver, id), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id = {1};", silver, id));
                cmd2.Transaction = tr;
                cmd2.ExecuteScalar();
                var cmd3 = new NpgsqlCommand(string.Format("update \"player\" set gold_coin=gold_coin-{0} where id={1};", gold, id), conn);
                Console.WriteLine(string.Format("update \"player\" set gold_coin=gold_coin-{0} where id = {1};", silver, id));
                cmd3.Transaction = tr;
                cmd3.ExecuteScalar();
                tr.Commit();
            }
            else //只买药水的情况，是不需要transaction的
            {
                foreach (var item in items)
                {
                    //status: 1 occupied; 2 dressing; 3 sale
                    var cmd = new NpgsqlCommand(string.Format("insert into \"treasure\"(name, type, effect, value, price, status, owner_id) values('{0}','{1}','{2}',{3},{4},'{5}',{6});",
                                                                item.name, item.type, item.effect, item.value, item.price, '1', id), conn);
                    Console.WriteLine(string.Format("insert into \"treasure\"(name, value, price, status, owner_id) values('{0}',{1},{2},'{3}',{4});",
                                                        item.name, item.value, item.price, '1', id));
                    cmd.ExecuteScalar();
                    sum += item.price;
                }
                var cmd2 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id={1};", sum, id), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id = {1};", sum, id));
                cmd2.ExecuteScalar();
            }
            
            return true;
        }

        public List<Treasure> GetInventory(int id)
        {
            List<Treasure> inventory = new List<Treasure>();
            var cmd = new NpgsqlCommand(string.Format("select * from treasure where owner_id = {0};", id), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Treasure result = new Treasure();
                Console.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}", reader["id"], reader["name"], reader["value"], reader["type"], reader["effect"]));
                result.id = Convert.ToInt32(reader["id"]);
                result.name = Convert.ToString(reader["name"]);
                result.value = Convert.ToInt32(reader["value"]);
                result.type = Convert.ToChar(reader["type"]);
                result.effect = Convert.ToChar(reader["effect"]);
                result.status = Convert.ToChar(reader["status"]);
                inventory.Add(result);
            }
            reader.Close();
            return inventory;
        }

        public Treasure GetTreasure(int dbid, string treasureName)
        {
            Console.WriteLine(string.Format("{0}, {1}", dbid, treasureName));
            var cmd = new NpgsqlCommand(string.Format("select * from treasure where owner_id = {0} and name = '{1}';", dbid, treasureName), conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            Treasure result = new Treasure();
            Console.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}", reader["id"], reader["name"], reader["value"], reader["type"], reader["effect"]));
            result.id = Convert.ToInt32(reader["id"]);
            result.name = Convert.ToString(reader["name"]);
            result.value = Convert.ToInt32(reader["value"]);
            result.type = Convert.ToChar(reader["type"]);
            result.effect = Convert.ToChar(reader["effect"]);
            result.status = Convert.ToChar(reader["status"]);
            reader.Close();
            return result;
        }

        public void ChangeStatusOn(int dbid, int treasureId)
        {
            var cmd = new NpgsqlCommand(string.Format("update treasure set status = '2' where owner_id = {0} and id = '{1}';", dbid, treasureId), conn);
            cmd.ExecuteScalar();
        }

        public void ChangeStatusOff(int dbid, int treasureId)
        {
            var cmd = new NpgsqlCommand(string.Format("update treasure set status = '1' where owner_id = {0} and id = '{1}';", dbid, treasureId), conn);
            cmd.ExecuteScalar();
        }

        public void playerExit(int dbid)
        {
            var cmd = new NpgsqlCommand(string.Format("update treasure set status = '1' where owner_id = {0};", dbid), conn);
            cmd.ExecuteScalar();
        }

        public void DeleteTreasure(int treasureId)
        {
            var cmd = new NpgsqlCommand(string.Format("delete from treasure where id = {0};", treasureId), conn);
            cmd.ExecuteScalar();
        }
        
        public List<MarketTreasure> GetMarket()
        {
            Console.WriteLine("GetMarket()");
            var cmd = new NpgsqlCommand("select * from market;", conn);
            List<MarketTreasure> market = new List<MarketTreasure>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MarketTreasure result = new MarketTreasure
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    value = Convert.ToInt32(reader["value"]),
                    type = Convert.ToChar(reader["type"]),
                    effect = Convert.ToChar(reader["effect"]),
                    status = Convert.ToChar(reader["status"]),
                    price = Convert.ToInt32(reader["price"]),
                    owner_id = Convert.ToInt32(reader["owner_id"]),
                    coinType = Convert.ToBoolean(reader["coinType"])
                };
                Console.WriteLine(result.id);
                market.Add(result);
            }
            reader.Close();
            return market;
        }

        public void MarketSell(MarketTreasure item)
        {
            Console.WriteLine("MarketSell");
            NpgsqlTransaction tr = conn.BeginTransaction();
            var cmd1 = new NpgsqlCommand(string.Format("delete from treasure where id = {0};", item.id), conn);
            var cmd2 = new NpgsqlCommand(string.Format("insert into \"market\"(name, type, effect, value, price, status, owner_id, id, coinType) values('{0}','{1}','{2}',{3}, {4},'{5}',{6}, {7}, {8});",
                                                       item.name, item.type, item.effect, item.value, item.price, '3', item.owner_id, item.id, item.coinType), conn);
            Console.WriteLine(string.Format("{0}, {1}, {2}", item.id, item.name, item.owner_id));
            cmd1.Transaction = tr;
            cmd1.ExecuteScalar();
            cmd2.Transaction = tr;
            cmd2.ExecuteScalar();
            tr.Commit();
        }
        public void MarketChange(MarketTreasure item)
        {
            Console.WriteLine("MarketChange");
            NpgsqlTransaction tr = conn.BeginTransaction();
            var cmd1 = new NpgsqlCommand(string.Format("delete from market where id = {0};", item.id), conn);
            var cmd2 = new NpgsqlCommand(string.Format("insert into \"market\"(name, type, effect, value, price, status, owner_id, id, coinType) values('{0}','{1}','{2}',{3}, {4},'{5}',{6}, {7}, {8});",
                                                       item.name, item.type, item.effect, item.value, item.price, '3', item.owner_id, item.id, item.coinType), conn);
            Console.WriteLine(string.Format("{0}, {1}, {2}, {3}", item.id, item.name, item.owner_id, item.price));
            cmd1.Transaction = tr;
            cmd1.ExecuteScalar();
            cmd2.Transaction = tr;
            cmd2.ExecuteScalar();
            tr.Commit();
        }
        public void MarketBuy(List<MarketTreasure> items, int id, int owner, bool type)
        {
            int sum = 0;
            int gold = 0;
            int silver = 0;
            if (type) //一旦有别的东西加入进来，就需要transaction了，知道了嘛
            {
                NpgsqlTransaction tr = (NpgsqlTransaction)conn.BeginTransaction();
                foreach (var item in items)
                {
                    //status: 1 occupied; 2 dressing; 3 sale
                    var cmd = new NpgsqlCommand(string.Format("insert into \"treasure\"(id, name, type, effect, value, price, status, owner_id) values({0}, '{1}','{2}','{3}',{4},{5},'{6}',{7});",
                                                                item.id, item.name, item.type, item.effect, item.value, item.price, '1', id), conn);
                    Console.WriteLine(string.Format("insert into \"treasure\"(id, name, type, effect, value, price, status, owner_id) values({0}, '{1}','{2}','{3}',{4},{5},'{6}',{7});",
                                                                item.id, item.name, item.type, item.effect, item.value, item.price, '1', id));
                    cmd.Transaction = tr;
                    cmd.ExecuteScalar();
                    if (!item.coinType) silver += item.price;
                    else gold += item.price;
                    var cmd4 = new NpgsqlCommand(string.Format("delete from \"market\" where id = {0};", item.id), conn);
                    cmd4.Transaction = tr;
                    cmd4.ExecuteScalar();
                }
                var cmd2 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id={1};", silver, id), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id = {1};", silver, id));
                cmd2.Transaction = tr;
                cmd2.ExecuteScalar();
                var cmd3 = new NpgsqlCommand(string.Format("update \"player\" set gold_coin=gold_coin-{0} where id={1};", gold, id), conn);
                Console.WriteLine(string.Format("update \"player\" set gold_coin=gold_coin-{0} where id = {1};", silver, id));
                cmd3.Transaction = tr;
                cmd3.ExecuteScalar();
                var cmd5 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin+{0} where id={1};", silver, owner), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin+{0} where id={1};", gold, owner));
                cmd5.Transaction = tr;
                cmd5.ExecuteScalar();
                var cmd6 = new NpgsqlCommand(string.Format("update \"player\" set gold_coin=gold_coin+{0} where id={1};", gold, owner), conn);
                Console.WriteLine(string.Format("update \"player\" set gold_coin=gold_coin+{0} where id = {1};", gold, owner));
                cmd6.Transaction = tr;
                cmd6.ExecuteScalar();
                tr.Commit();
            }
            else 
            {
                foreach (var item in items)
                {
                    //status: 1 occupied; 2 dressing; 3 sale
                    var cmd = new NpgsqlCommand(string.Format("insert into \"treasure\"(id, name, type, effect, value, price, status, owner_id) values({0}, '{1}','{2}','{3}',{4},{5},'{6}',{7});",
                                                                item.id, item.name, item.type, item.effect, item.value, item.price, '1', id), conn);
                    Console.WriteLine(string.Format("insert into \"treasure\"(id, name, type, effect, value, price, status, owner_id) values({0}, '{1}','{2}','{3}',{4},{5},'{6}',{7});",
                                                                item.id, item.name, item.type, item.effect, item.value, item.price, '1', id));
                    cmd.ExecuteScalar();
                    sum += item.price;
                    var cmd3 = new NpgsqlCommand(string.Format("delete from \"market\" where id = {0};", item.id), conn);
                    cmd3.ExecuteScalar();
                }
                var cmd2 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id={1};", sum, id), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id = {1};", sum, id));
                cmd2.ExecuteScalar();
                var cmd4 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin+{0} where id={1};", sum, owner), conn);
                Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin+{0} where id = {1};", sum, owner));
                cmd4.ExecuteScalar();
            }
        }
        public List<MarketTreasure> GetMyMarket(int dbid)
        {
            Console.WriteLine("GetMarket()");
            var cmd = new NpgsqlCommand(string.Format("select * from market where owner_id={0};",dbid), conn);
            List<MarketTreasure> market = new List<MarketTreasure>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MarketTreasure result = new MarketTreasure
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    value = Convert.ToInt32(reader["value"]),
                    type = Convert.ToChar(reader["type"]),
                    effect = Convert.ToChar(reader["effect"]),
                    status = Convert.ToChar(reader["status"]),
                    price = Convert.ToInt32(reader["price"]),
                    owner_id = Convert.ToInt32(reader["owner_id"]),
                    coinType = Convert.ToBoolean(reader["coinType"])
                };
                Console.WriteLine(string.Format("GetMyMarket: {0}", result.id));
                market.Add(result);
            }
            reader.Close();
            return market;
        }

        public void Withdraw(MarketTreasure t)
        {

        }
		//Database.Instacne.AddGold(
		public void AddGolds(int golds, int dbid)
		{
			var cmd = new NpgsqlCommand(string.Format("update player set gold_coin=gold_coin+{0} where id = {1};", golds, dbid), conn);
			cmd.ExecuteScalar();
			Console.WriteLine(string.Format("add gold nums =  {0}", golds));
		}

		public void MinesGolds(int golds, int dbid)
		{
			var cmd = new NpgsqlCommand(string.Format("update player set gold_coin=gold_coin-{0} where id = {1};", golds, dbid), conn);
			cmd.ExecuteScalar();
			Console.WriteLine(string.Format("Player {0} Mines nums = {1}", dbid, golds));
		}

        public List<AddFriend> GetAddFriend(int dbid)
        {
            Console.WriteLine("GetAddFriend()");
            var cmd = new NpgsqlCommand(string.Format("select * from friend, player where friend.request=player.id and response = {0} and status = 0;", dbid), conn);
            List<AddFriend> friendList = new List<AddFriend>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AddFriend result = new AddFriend
                {
                    dbID = Convert.ToInt32(reader["request"]),
                    name = Convert.ToString(reader["name"]),
                    info = Convert.ToString(reader["message"])
                };
                friendList.Add(result);
            }
            reader.Close();
            return friendList;
        }

        public void AccFriend(int request, int response)
        {
            Console.WriteLine("AccFriend()");
            var cmd = new NpgsqlCommand(string.Format("update friend set status = 1 where (response = {0} and request = {1}) or (response = {1} and request = {0});", request, response, response, request), conn);
            cmd.ExecuteScalar();
        }

        public void RejFriend(int request, int response)
        {
            Console.WriteLine("RejFriend()");
            var cmd = new NpgsqlCommand(string.Format("update friend set status = 2 where (response = {0} and request = {1}) or (response = {1} and request = {0});", request, response, response, request), conn);
            cmd.ExecuteScalar();
        }

        public bool findFriend(string name)
        {
            Console.WriteLine("findFriend()");
            var cmd = new NpgsqlCommand(string.Format("select * from player where name = '{0}';", name), conn);
            int count = 0;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            if(count == 0)
                return false;
            return true;
        }

        public int insertFriend(string response, int request, string message)
        {
            int id = 0;
            int count = 0;
            var cmd = new NpgsqlCommand(string.Format("select * from player where name = '{0}';", response), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
            }
            reader.Close();
            var cmd0 = new NpgsqlCommand(string.Format("select * from friend where (response = {0} and request = {1}) or (response = {1} and request = {0});", id, request), conn);
            var reader0 = cmd0.ExecuteReader();
            while (reader0.Read())
            {
                count++;
            }
            reader0.Close();
            var cmd1 = new NpgsqlCommand(string.Format("insert into friend(response, request, status, message) values({0}, {1}, 0, '{2}');", id, request, message), conn);
            cmd1.ExecuteScalar();
            return count;
        }

        public List<int> getMyFriend(int dbid)
        {
            Console.WriteLine("getMyFriend()");
            var cmd = new NpgsqlCommand(string.Format("select * from friend where (friend.request={0} or friend.response={0}) and status = 1;", dbid), conn);
            List<int> friendList = new List<int>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["request"]) != dbid)
                    friendList.Add(Convert.ToInt32(reader["request"]));
                else
                    friendList.Add(Convert.ToInt32(reader["response"]));
            }
            reader.Close();
            return friendList;
        }
    }
}