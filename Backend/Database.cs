﻿using System;
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

        public bool BuyItems(List<Treasure> items, int id)
        {
            int sum = 0;

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
                sum += item.price;
            }
            var cmd2 = new NpgsqlCommand(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id={1};", sum, id), conn);
            Console.WriteLine(string.Format("update \"player\" set silver_coin=silver_coin-{0} where id = {1};", sum, id));
            cmd2.Transaction = tr;
            cmd2.ExecuteScalar();
            tr.Commit();
            return true;
        }

        public List<String> GetInventory(int id)
        {
            List<String> inventory = new List<String>();
            var cmd = new NpgsqlCommand(string.Format("select name from treasure where owner_id = {0};", id), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                inventory.Add((string)reader["name"]);
            }
            reader.Close();
            return inventory;
        }

        public Treasure GetTreasure(int dbid, string treasureName)
        {
            //dbid = 2;
            //treasureName = "Elixir_1";
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

        public void ChangeStatusOn(int dbid, String treasureName)
        {
            var cmd = new NpgsqlCommand(string.Format("update treasure set status = '2' where owner_id = {0} and name = '{1}';", dbid, treasureName), conn);
        }

        public void ChangeStatusOff(int dbid, String treasureName)
        {
            var cmd = new NpgsqlCommand(string.Format("update treasure set status = '1' where owner_id = {0} and name = '{1}';", dbid, treasureName), conn);
        }
    }
}