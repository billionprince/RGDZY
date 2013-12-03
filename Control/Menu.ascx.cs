using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;

namespace RGDZY.control
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string getUsername()
        {
            string myname;
            if (System.Web.HttpContext.Current.Session["_Login_Name"] == null)
                myname = "Guest";
            else
                myname = (string)System.Web.HttpContext.Current.Session["_Login_Name"];
            return myname;
        }

        // Daniel: DB connection part of this function should be located in a single class
        public uint getPriority()
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
                User user = query.ToList()[0];

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