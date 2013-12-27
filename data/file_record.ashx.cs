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
    /// Summary description for file_record
    /// </summary>
    public class file_record : IHttpHandler
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

        public void get_file_record(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<List<string>> rec = new List<List<string>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    Table<PrintFile> table_printfile = dc.GetTable<PrintFile>();
                    try
                    {
                        var query = from r in table_printfile select r;
                        foreach (var obj in query)
                        {
                            List<string> evt = new List<string>();
                            evt.Add(obj.username);
                            evt.Add(obj.time);
                            evt.Add(obj.filename);
                            evt.Add(obj.single == 0 ? "Single":"Double");
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}