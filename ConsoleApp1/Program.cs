using System;
using Npgsql;

namespace ConsoleApp1
{

    class Program
    {
        public static bool RegisterUser(string username, string password)
        {
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=0515;Database=game";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(string.Format("insert into \"uu\"(name, password) values('{0}', '{1}');", username, password), conn);
            var ret = cmd.ExecuteNonQuery();
            if (ret != 0)
            {
                Console.WriteLine("success");
                return true;
            }                
            else return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=0515;Database=game";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
           // var cmd = new NpgsqlCommand("SELECT * From test2 Where", conn);
            string username = "ffffff";
            var cmd = new NpgsqlCommand(string.Format("select * from uu where name = '{0}';", username), conn);
            var reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
                count++;
            }
            Console.WriteLine("count = {0}", count);
            Console.ReadLine();
            //RegisterUser("ff", "19970515");
        }
    }
}
