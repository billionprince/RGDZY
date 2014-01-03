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
    /// Summary description for project
    /// </summary>
    public class project : IHttpHandler
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void get_project_detail(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Project_chat> table_project_char = dc.GetTable<Project_chat>();
                        Table<User> table_user = dc.GetTable<User>();
                        int project_id = int.Parse(context.Request["id"]);
                        //var query = from r1 in table_project_char from r2 in table_user 
                        //            where r1.owner == r2.RealName && r1.project_id == project_id 
                        //            select new {
                        //                owner = r1.owner,
                        //                content = r1.chat_content,
                        //                time = r1.chat_time,
                        //                image = r2.Link 
                        //            };
                        var query = from r in table_project_char where r.project_id == project_id select r;
                        foreach (var obj in query)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.owner);
                            //evt.Add("content", obj.content);
                            //evt.Add("time", obj.time);
                            //evt.Add("image", obj.image);
                            evt.Add("content", obj.chat_content);
                            evt.Add("time", obj.chat_time);
                            evt.Add("image", "user_data/"+obj.owner+"/a_" + obj.owner + ".jpg");
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

        public void put_project_detail_chat(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Project_chat> table_project_char = dc.GetTable<Project_chat>();
                        Project_chat obj = new Project_chat();
                        obj.project_id = int.Parse(context.Request["project_id"]);
                        obj.owner = context.Request["name"];
                        obj.chat_time = context.Request["time"];
                        obj.chat_content = context.Request["content"];
                        table_project_char.InsertOnSubmit(obj);
                        dc.SubmitChanges();
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
    }
}