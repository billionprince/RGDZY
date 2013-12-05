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

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for login
    /// </summary>
    public class login_service : IHttpHandler, IRequiresSessionState
    {
        private string ValidateExec(string username, string password, out uint authority)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            try{
                Table<User> tu = dc.GetTable<User>();
                var query = from u in tu
                            where u.Name == username
                            select u;
                User user = query.ToList()[0];

                // found username
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        authority = (uint)user.Authority;
                        return "Success";
                    }
                }
                authority = 0x0;
                return "False";
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

        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["command"];
            if (command != null)
            {
                System.Reflection.MethodInfo method = this.GetType().GetMethod(command);
                if (method != null)
                {
                    method.Invoke(this, new object[] { context });
                    return;
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Error");
        }

        public void get_validate(HttpContext context)
        {
            string un = context.Request["username"];
            string pw = context.Request["password"];
            string resp;
            uint ar;
            context.Response.ContentType = "text/plain";

            resp = ValidateExec(un, pw, out ar);
            if (resp == null)
                resp = "Error";
            else if (resp == "Success")
            {
                context.Session["_Login_Name"] = un;
                context.Session["_Login_Authority"] = ar;
            }
            
            context.Response.Write(resp);
            Console.WriteLine(resp);
        }

        public void profile_update(HttpContext context)
        {
            string un = context.Request["username"];
            string pw = context.Request["password"];
            string resp;
            uint ar;
            context.Response.ContentType = "text/plain";

            resp = ValidateExec(un, pw, out ar);
            if (resp == null)
                resp = "Error";
            else if (resp == "Success")
            {
                context.Session["_Login_Name"] = un;
                context.Session["_Login_Authority"] = ar;
            }

            context.Response.Write(resp);
            Console.WriteLine(resp);
        }

        public void get_logout(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["_Login_Name"] != null)
                context.Session.Remove("_Login_Name");

            if (context.Session["_Login_Authority"] != null)
                context.Session.Remove("_Login_Authority");

            //context.Session.RemoveAll();

            //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //context.Response.Cache.SetNoServerCaching();
            //context.Response.Cache.SetNoStore();

            context.Response.Redirect("../login.aspx");
            context.Response.End();
        }

        public void get_username(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string username = Authority.getUsername();
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(username));
        }

        public void get_authority(HttpContext context)
        {
            //return Authority.getAuthority();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
