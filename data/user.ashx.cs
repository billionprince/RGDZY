using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RGDZY.control;
using System.Data.Linq;
using System.Data.SqlClient;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for user
    /// </summary>
    public class user : IHttpHandler
    {

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

        public void get_user_group(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    Table<UserGroup> table_usergroup = dc.GetTable<UserGroup>();
                    Table<User> table_user = dc.GetTable<User>();
                    try
                    {
                        var group = (from r in table_usergroup select r.Groupname).Distinct().ToList();
                        foreach (var obj in group)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("groupname", obj);
                            var query = (from r in table_usergroup from p in table_user where r.Username == p.Name && r.Groupname == obj select p.RealName).ToList();
                            evt.Add("username", query);
                            rec.Add(evt);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
        }

        public static string[] get_user_name(string[] rname)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    var uname = new List<string>();
                    var utable = dc.GetTable<User>();

                    try
                    {
                        foreach (var str in rname)
                        {
                            uname.Add(utable.First(o => o.RealName == str).Name);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    return uname.ToArray();
                }
            }

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