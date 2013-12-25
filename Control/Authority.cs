using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using System.Web.SessionState;
using System.Data.Linq;
using RGDZY.control;
using System.Web.Script.Serialization;

namespace RGDZY.control
{
    public class Authority
    {
        public const uint A_GUEST =     0x0;
        public const uint A_NORMAL =    0x1;
        public const uint A_SCHEDULE =  (0x1 << 1);
        public const uint A_DEVICE =    (0x1 << 2);
        public const uint A_PROJECT =   (0x1 << 3);
        public const uint A_ACCOUNT =   (0x1 << 4);
        public const uint A_ADMIN =     (0x1 << 5);

        static public string getUsername()
        {
            string myname;
            if (System.Web.HttpContext.Current.Session["_Login_Name"] == null)
                myname = "Guest";
            else
                myname = (string)System.Web.HttpContext.Current.Session["_Login_Name"];
            return myname;
        }

        // Daniel: DB connection part of this function should be located in a single class
        static public uint getAuthority()
        {
            string username;
            uint priority;
            if ((username = (string)System.Web.HttpContext.Current.Session["_Login_Name"]) == null)
                return 0;

            if (System.Web.HttpContext.Current.Session["_Login_Authority"] != null)
            {
                priority = (uint)System.Web.HttpContext.Current.Session["_Login_Authority"];
                return priority;
            }
            // fetch DB if cannot get priority in Session
            priority = 0x0;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            try
            {
                Table<User> tu = dc.GetTable<User>();
                var query = from u in tu
                            where u.Name == username
                            select u;
                User user = query.FirstOrDefault();

                // found username
                if (user != null)
                {
                    priority = (uint)user.Authority;
                }
                return priority;
            }
            catch (System.Exception exMsg)
            {
                string msg = "Error occured while executing:";
                msg += exMsg.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }
    }
}