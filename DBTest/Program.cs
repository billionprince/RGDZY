using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the Pool
            DBConnectionSingletion pool = DBConnectionSingletion.Instance;

            //Borrow the SqlConnection object from the pool
            SqlConnection conn = pool.BorrowDBConnection();

            DataContext dc = new DataContext(conn);
            Table<User> tu = dc.GetTable<User>();
            var query = from u in tu
                        where u.Id < 100
                        select u;
            foreach (var u in query)
            {
                Console.WriteLine("{0} {1}\n", u.Id, u.Name);
            }

            //Return the Connection to the pool after using it
            pool.ReturnDBConnection(conn);

            Console.ReadLine();
        }
    }
}
