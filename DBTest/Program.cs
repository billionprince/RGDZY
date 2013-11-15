using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Initial Catalog='testdb';Server='202.120.40.100,10433';User ID='sa';Password='sjtu_007'";
            //string connectionString = "Initial Catalog='testdb';Server='192.168.1.104,1433';User ID='sa';Password='sjtu_007'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("connected");
                    SqlCommand sqlcmd = connection.CreateCommand();

                    sqlcmd.CommandText = "select top 10 * from Person;";

                    SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                    while (sqlreader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}", sqlreader["firstname"], sqlreader["lastname"]);
                    }
                    sqlreader.Close();
                    connection.Close();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
