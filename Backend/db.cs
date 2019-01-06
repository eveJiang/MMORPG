using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Common;
using Backend.Game;

namespace Backend
{
    class db : Singleton<db>
    {
        public NpgsqlConnection Connect(string ip = "219.228.148.179", int port = 5432)
        {
            var connString = string.Format("Host=" + ip + ";Port={0:D4};Username=postgres;Password=0515;Database=game", port);
            var conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw (e);
            }
            return conn;
        }

        public int RegisterUser(string username, string password, NpgsqlConnection conn)
        {
            int count = 0;
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Select * from player where name = @username;";
                cmd.Parameters.AddWithValue("username", username);
                var res = cmd.ExecuteScalar();
                if(res != null)
                {
                    return 2;
                }
            }
            using (var cmd2 = conn.CreateCommand())
            {
                cmd2.CommandText = "insert into player(name, password, gold_coin, silver_coin) values(@name, @password, 2000, 1000);";
                cmd2.Parameters.AddWithValue("name", username);
                cmd2.Parameters.AddWithValue("password", password);
                var ret = cmd2.ExecuteNonQuery();
                if (ret != 0)
                    return 1;  //success
                else return 0; //fail
            }
            
        }

        public bool LoginUser(string username, string password, NpgsqlConnection conn)
        {
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from player where name = @name and password = @password;";
                cmd.Parameters.AddWithValue("name", username);
                cmd.Parameters.AddWithValue("password", password);
                var res = cmd.ExecuteScalar();
                if (res == null)
                    return false;
                return true;
            }
        }

        public int GetID(string username, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select id from player where name = @name;";
                cmd.Parameters.AddWithValue("name", username);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
        }

        public int GetSilverCoins(int id, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select silver_coin from player where id = @id;";
                cmd.Parameters.AddWithValue("id", id);
                int coins = Convert.ToInt32(cmd.ExecuteScalar());
                return coins;
            }
        }

        public int GetGoldCoins(int id, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select gold_coin from player where id = @id;";
                cmd.Parameters.AddWithValue("id", id);
                int coins = Convert.ToInt32(cmd.ExecuteScalar());
                return coins;
            }
        }

        public bool BuyItems(List<Treasure> items, int gold, int silver, int id, int mark, NpgsqlConnection conn)
        {
            if(gold != 0)
            {
                using (var trans = conn.BeginTransaction())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select silver_coin from player where id = @id;";
                        cmd.Parameters.AddWithValue("id", id);
                        int coins = Convert.ToInt32(cmd.ExecuteScalar());
                        if (coins < silver)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }                    
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select gold_coin from player where id = @id;";
                        cmd.Parameters.AddWithValue("id", id);
                        int coins = Convert.ToInt32(cmd.ExecuteScalar());
                        if (coins < gold)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    foreach (var item in items)                   
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "insert into treasure(name, type, effect, value, price, status, owner_id) values(@name, @type, @effect, @value, @price, @status, @owner_id);";
                            cmd.Parameters.AddWithValue("name", item.name);
                            cmd.Parameters.AddWithValue("type", item.type);
                            cmd.Parameters.AddWithValue("effect", item.effect);
                            cmd.Parameters.AddWithValue("value", item.value);
                            cmd.Parameters.AddWithValue("price", item.price);
                            cmd.Parameters.AddWithValue("status", '1');
                            cmd.Parameters.AddWithValue("owner_id", id);
                            cmd.ExecuteScalar();
                        }
                    }
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update player set silver_coin = silver_coin - @coin where id = @id;";
                        cmd.Parameters.AddWithValue("coin", silver);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteScalar();
                    }
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update player set gold_coin = gold_coin - @coin where id = @id;";
                        cmd.Parameters.AddWithValue("coin", gold);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteScalar();
                    }
                    trans.Commit();
                }
            }
            else
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select silver_coin from player where id = @id;";
                    cmd.Parameters.AddWithValue("id", id);
                    int coins = Convert.ToInt32(cmd.ExecuteScalar());
                    if (coins < silver)
                    {
                        return false;
                    }
                }
                foreach (var item in items)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into treasure(name, type, effect, value, price, status, owner_id) values(@name, @type, @effect, @value, @price, @status, @owner_id);";
                        cmd.Parameters.AddWithValue("name", item.name);
                        cmd.Parameters.AddWithValue("type", item.type);
                        cmd.Parameters.AddWithValue("effect", item.effect);
                        cmd.Parameters.AddWithValue("value", item.value);
                        cmd.Parameters.AddWithValue("price", item.price);
                        cmd.Parameters.AddWithValue("status", '1');
                        cmd.Parameters.AddWithValue("owner_id", id);
                        cmd.ExecuteScalar();
                    }
                    
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update player set silver_coin = silver_coin - @coin where id = @id;";
                    cmd.Parameters.AddWithValue("coin", silver);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteScalar();
                }
            }
            return true;
        }

        public List<Treasure> GetInventory(int id, NpgsqlConnection conn)
        {
            List<Treasure> inventory = new List<Treasure>();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from treasure where owner_id = @id order by id asc;";
                cmd.Parameters.AddWithValue("id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Treasure result = new Treasure();
                    result.id = Convert.ToInt32(reader["id"]);
                    result.name = Convert.ToString(reader["name"]);
                    result.value = Convert.ToInt32(reader["value"]);
                    result.type = Convert.ToChar(reader["type"]);
                    result.effect = Convert.ToChar(reader["effect"]);
                    result.status = Convert.ToChar(reader["status"]);
                    inventory.Add(result);
                }
                reader.Close();
            }
            return inventory;
        }

        public Treasure GetTreasure(int dbid, string treasureName, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from treasure where owner_id = @id and name = @name;";
                cmd.Parameters.AddWithValue("id", dbid);
                cmd.Parameters.AddWithValue("name", treasureName);
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
        }

        public void ChangeStatusOn(int dbid, int treasureId, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update treasure set status = '2' where owner_id = @dbid and id = @treasure_id;";
                cmd.Parameters.AddWithValue("dbid", dbid);
                cmd.Parameters.AddWithValue("treasure_id", treasureId);
                cmd.ExecuteScalar();
            }
        }

        public void ChangeStatusOff(int dbid, int treasureId, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update treasure set status = '1' where owner_id = @dbid and id = @treasure_id;";
                cmd.Parameters.AddWithValue("dbid", dbid);
                cmd.Parameters.AddWithValue("treasure_id", treasureId);
                cmd.ExecuteScalar();
            }
        }

        public void playerExit(int dbid, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update treasure set status = '1' where owner_id = @dbid;";
                cmd.Parameters.AddWithValue("dbid", dbid);
                cmd.ExecuteScalar();
            }
        }

        public void DeleteTreasure(int treasureId, NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "delete from treasure where id = @id;";
                cmd.Parameters.AddWithValue("id", treasureId);
                cmd.ExecuteScalar();
            }
        }
        
        public List<MarketTreasure> GetMarket(NpgsqlConnection conn)
        {
            Console.WriteLine("GetMarket()");
            List<MarketTreasure> market = new List<MarketTreasure>();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select * from market;";
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
            }         
            return market;
        }

        public void MarketSell(MarketTreasure item, NpgsqlConnection conn)
        {
            Console.WriteLine("MarketSell");
            NpgsqlTransaction tr = conn.BeginTransaction();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "delete from treasure where id = @id;";
                cmd.Parameters.AddWithValue("id", item.id);
                cmd.Transaction = tr;
                cmd.ExecuteScalar();
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "insert into \"market\"(name, type, effect, value, price, status, owner_id, id, coinType) values(@name, @type, @effect, @value, @price, @status, @owner_id, @id, @coinType);";
                cmd.Parameters.AddWithValue("name", item.name);
                cmd.Parameters.AddWithValue("type", item.type);
                cmd.Parameters.AddWithValue("effect", item.effect);
                cmd.Parameters.AddWithValue("value", item.value);
                cmd.Parameters.AddWithValue("price", item.price);
                cmd.Parameters.AddWithValue("status", '3');
                cmd.Parameters.AddWithValue("owner_id", item.owner_id);
                cmd.Parameters.AddWithValue("id", item.id);
                cmd.Parameters.AddWithValue("coinType", item.coinType);
                cmd.Transaction = tr;
                cmd.ExecuteScalar();
            }
            Console.WriteLine(string.Format("{0}, {1}, {2}", item.id, item.name, item.owner_id));
            tr.Commit();
        }
        public void MarketChange(MarketTreasure item, NpgsqlConnection conn)
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
        public void MarketBuy(List<MarketTreasure> items, int id, int owner, bool type, NpgsqlConnection conn)
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
                    Console.WriteLine(string.Format("delete from \"market\" where id = {0};", item.id));
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
                var cmdGetGold = new NpgsqlCommand("select gold_coin from player where id = " + id.ToString() + ";", conn);
                cmdGetGold.Transaction = tr;
                var reader = cmdGetGold.ExecuteReader();
                int coins = 0;
                while(reader.Read())
                    coins = Convert.ToInt32(reader["gold_coin"]);
                reader.Close();
                //int coins = Convert.ToInt32(cmdGetGold.ExecuteReader());
                if (coins < gold && id != owner)
                {
                    tr.Rollback();
                    Console.WriteLine("rollback");
                    return;
                }
                
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
        public List<MarketTreasure> GetMyMarket(int dbid, NpgsqlConnection conn)
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
		public void AddGolds(int golds, int dbid, NpgsqlConnection conn)
		{
			var cmd = new NpgsqlCommand(string.Format("update player set gold_coin=gold_coin+{0} where id = {1};", golds, dbid), conn);
			cmd.ExecuteScalar();
			Console.WriteLine(string.Format("add gold nums =  {0}", golds));
		}

		public void MinesGolds(int golds, int dbid, NpgsqlConnection conn)
		{
			var cmd = new NpgsqlCommand(string.Format("update player set gold_coin=gold_coin-{0} where id = {1};", golds, dbid), conn);
			cmd.ExecuteScalar();
			Console.WriteLine(string.Format("Player {0} Mines nums = {1}", dbid, golds));
		}

        public List<AddFriend> GetAddFriend(int dbid, NpgsqlConnection conn)
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
                    info = Convert.ToString(reader["message"]),
                    asTeam = Convert.ToBoolean(reader["asTeam"])
                };
                friendList.Add(result);
            }
            reader.Close();
            return friendList;
        }

        public bool AccFriend(int request, int response, NpgsqlConnection conn)
        {
            Console.WriteLine("AccFriend()");
            var cmd = new NpgsqlCommand(string.Format("update friend set status = 1 where (response = {0} and request = {1}) or (response = {1} and request = {0});", request, response, response, request), conn);
            cmd.ExecuteScalar();
            var cancelteam = new NpgsqlCommand(string.Format("update friend set asTeam = false where ((response = {0} and request = {1}) or (response = {1} and request = {0})) and asTeam=true;", request, response, response, request), conn);
            cancelteam.ExecuteScalar();
            var cmd1 = new NpgsqlCommand(string.Format("select * from friend where (response = {0} and request = {1}) or (response = {1} and request = {0});", request, response, response, request), conn);
            var reader = cmd.ExecuteReader();
            bool ret = false;
            while (reader.Read())
            {
                ret = Convert.ToBoolean(reader["asTeam"]);
            }
            reader.Close();
            return ret;
        }

        public void RejFriend(int request, int response, NpgsqlConnection conn)
        {
            Console.WriteLine("RejFriend()");
            var cmd = new NpgsqlCommand(string.Format("update friend set status = 2 where (response = {0} and request = {1}) or (response = {1} and request = {0});", request, response, response, request), conn);
            cmd.ExecuteScalar();
        }

        public bool findFriend(string name, NpgsqlConnection conn)
        {
            Console.WriteLine("findFriend()");
            var cmd = new NpgsqlCommand("select * from player where name = @name;", conn);
            cmd.Parameters.AddWithValue("name", name);
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

        public int insertFriend(string response, int request, string message, bool asTeam, NpgsqlConnection conn)
        {
            int id = 0;
            int count = 0;
            var cmd = new NpgsqlCommand("select * from player where name = @response;", conn);
            cmd.Parameters.AddWithValue("response", response);
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
            if (id == request)
                count++;
            reader0.Close();
            var cmd1 = new NpgsqlCommand(string.Format("insert into friend(response, request, status, message, asTeam) values({0}, {1}, 0, @message, @team);", id, request), conn);
            cmd1.Parameters.AddWithValue("message", message);
            cmd1.Parameters.AddWithValue("team", asTeam);
            cmd1.ExecuteScalar();
            return count;
        }

        public List<int> getMyFriend(int dbid, NpgsqlConnection conn)
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

        public int getTeammate(int dbid, NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(string.Format("select * from friend where (friend.request={0} or friend.response={0}) and asTeam = true;", dbid), conn);
            var reader = cmd.ExecuteReader();
            int ret = -1;
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["request"]) != dbid)
                    ret = Convert.ToInt32(reader["request"]);
                else
                    ret = Convert.ToInt32(reader["response"]);
            }
            reader.Close();
            return ret;
        }
    }
}