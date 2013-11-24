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
            string connectionString = String.Format("Initial Catalog='{0}';Server='{1},{2}';User ID='{3}';Password='sjtu_007'",
                Properties.Settings.Default.DBName,
                Properties.Settings.Default.DBIpAddress,
                Properties.Settings.Default.DBPort,
                Properties.Settings.Default.DBUser);
            Console.WriteLine("{0}", connectionString);

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
