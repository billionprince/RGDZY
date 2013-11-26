using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using System.Web.SessionState;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for get_user_calendar
    /// </summary>
    public class login : IHttpHandler, IRequiresSessionState
    {
        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }

        private string ValidateExec(string username, string password)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            
            try
            {
                if ((username == null) || (password == null))
                    return "Error";
                conn.Open();
                string sql = "select * from [dbo].[User] where Name=@username;";
                SqlCommand scmd = new SqlCommand(sql, conn);
                SqlParameter[] param = new SqlParameter[1];
                
                param[0] = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                //param[1] = new SqlParameter("@password", SqlDbType.NVarChar, 50);
                param[0].Value = username;
                //param[1].Value = password;

                for (int i = 0; i < param.Length; i++)
                {
                    scmd.Parameters.Add(param[i]);
                }
                scmd.CommandType = CommandType.Text;
                SqlDataReader dr = scmd.ExecuteReader();
                //scmd.ExecuteNonQuery();
                if (dr.Read())
                {
                    string fromDB = dr["Password"].ToString();
                    if (password == fromDB)
                    {
                        //string account = context.Session["userAccount"].ToString();
                        //Login Success
                        return "Success";
                    }
                }
                return "Failed";
            }
            catch (System.Data.SqlClient.SqlException exMsg)
            {
                string msg = "Error occured while executing";
                msg += exMsg.Message;
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["command"];
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Validate Failed");
            //context.Response.End();   
            //return;
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
            context.Response.ContentType = "text/plain";

            resp = ValidateExec(un, pw);
            if (resp == null)
                resp = "Error";
            else if (resp == "Success")
            {
                context.Session["_Login_Name"] = un;
            }
            
            context.Response.Write(resp);
            Console.WriteLine(resp);
            //context.Response.Close();
            //if (un == "admin" && pw == "admin")
            //    context.Response.Write("Succeed");
            //else
            //    context.Response.Write("Validate Failed");
        }

        public void get_logout(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["_Login_Name"] != null)
                context.Session.Remove("_Login_Name");

            if (context.Session["_Login_Arthority"] != null)
                context.Session.Remove("_Login_Arthority");

            //context.Session.RemoveAll();

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoServerCaching();
            context.Response.Cache.SetNoStore();

            context.Response.Redirect("~/login.aspx");
            context.Response.Close();
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
