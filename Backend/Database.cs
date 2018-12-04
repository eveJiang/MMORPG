using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Backend
{
    class Database:Singleton<Database>
    {
        private NpgsqlConnection conn = null;

        public bool Connect(string ip="localhost", int port=5432)
        {
            
            var connString = string.Format("Host=" + ip + ";Port={0:D4};Username=postgres;Password=0515;Database=game", port);
            conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw (e);
            }
            Console.WriteLine("Connected to database.");
            return true;
        }

        public int RegisterUser(string username, string password)
        {
            int count = 0;
            var cmd = new NpgsqlCommand(string.Format("select * from uu where name = '{0}';", username), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            if (count != 0)
                return 2;   //user name error
            var cmd2 = new NpgsqlCommand(string.Format("insert into \"uu\"(name, password) values('{0}', '{1}');", username, password), conn);
            var ret = cmd2.ExecuteNonQuery();
            if (ret != 0)
                return 1;  //success
            else return 0; //fail
        }

        public bool LoginUser(string username, string password)
        {
            int count = 0;
            var cmd = new NpgsqlCommand(string.Format("select * from uu where name = '{0}' and password = '{1}';", username, password), conn);
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
    }
}
