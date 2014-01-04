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
    /// Summary description for search
    /// </summary>
    public class search : IHttpHandler
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

        public void get_search_result(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, List<Dictionary<string, object>>> rec = new Dictionary<string, List<Dictionary<string, object>>>();
            string str = context.Request["str"];
            search_schedule(str, ref rec);
            search_device(str, ref rec);
            search_project(str, ref rec);
            search_file(str, ref rec);
            search_account(str, ref rec);
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
            
        }

        private void search_schedule(string str, ref Dictionary<string, List<Dictionary<string, object>>> rec)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                        int typ = reversetype(str);
                        var q1 = from r in table_calendar where r.Title.Contains(str) || r.Type == typ select r;
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        calendar cal = new calendar();
                        foreach (var obj in q1)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("title", obj.Title);
                            evt.Add("type", calendar.typelist[obj.Type]);
                            evt.Add("time", cal.show_time(obj.Start, obj.Type, obj.Allday));
                            evt.Add("status", cal.checkstatus(obj.Start, obj.Type, obj.Allday));
                            lst.Add(evt);
                        }
                        rec.Add("schedule", lst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private void search_device(string str, ref Dictionary<string, List<Dictionary<string, object>>> rec)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Device> table_device = dc.GetTable<Device>();
                        Table<DeviceUse> table_deviceuse = dc.GetTable<DeviceUse>();
                        var q = from r in table_device
                                join p in table_deviceuse on r.Id equals p.DeviceId 
                                into temp
                                from x in temp.DefaultIfEmpty()
                                where r.Remark.Contains(str) || r.AssetNum.Contains(str) || r.Type.Contains(str)
                                select new
                                {
                                    asset = r.AssetNum,
                                    type = r.Type,
                                    remark = r.Remark,
                                    owner = x != null ? x.UserId : null
                                };
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        foreach (var obj in q)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("asset", obj.asset.Length == 0 ? null : obj.asset);
                            evt.Add("type", obj.type.Length == 0 ? null : obj.type);
                            evt.Add("owner", obj.owner);
                            evt.Add("remark", obj.remark.Length == 0 ? null : obj.remark);
                            lst.Add(evt);
                        }
                        rec.Add("device", lst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private void search_project(string str, ref Dictionary<string, List<Dictionary<string, object>>> rec)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Project> table_project = dc.GetTable<Project>();
                        var q = from r in table_project where r.FullName.Contains(str) || r.Name.Contains(str) || r.Link.Contains(str) select r;
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        foreach (var obj in q)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.FullName);
                            evt.Add("des", obj.Description);
                            evt.Add("link", obj.Link);
                            evt.Add("id", obj.Id);
                            evt.Add("par", obj.Advisor);
                            lst.Add(evt);
                        }
                        rec.Add("project", lst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private void search_file(string str, ref Dictionary<string, List<Dictionary<string, object>>> rec)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<PrintFile> table_printfile = dc.GetTable<PrintFile>();
                        var q = from r in table_printfile where r.filename.Contains(str) || r.username.Contains(str) select r;
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        calendar cal = new calendar();
                        foreach (var obj in q)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.filename);
                            evt.Add("owner", obj.username);
                            evt.Add("time", obj.time);
                            lst.Add(evt);
                        }
                        rec.Add("file", lst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private void search_account(string str, ref Dictionary<string, List<Dictionary<string, object>>> rec)
        {

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<User> table_user = dc.GetTable<User>();
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        var q = from r in table_user
                                where r.Name.Contains(str) || r.RealName.Contains(str) ||
                                    r.StudentId.Contains(str) || r.Introduction.Contains(str) ||
                                    r.Link.Contains(str) || r.Hometown.Contains(str) ||
                                    r.University.Contains(str) || r.Email.Contains(str) ||
                                    r.Phone.Contains(str)
                                select r;
                        foreach (var obj in q)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.RealName);
                            evt.Add("team", obj.GroupName);
                            evt.Add("email", obj.Email);
                            evt.Add("telephone", obj.Phone);
                            evt.Add("link", obj.Link);
                            lst.Add(evt);
                        }
                        rec.Add("account", lst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private int reversetype(string str)
        {
            if (str.Contains("once")) return 0;
            else if (str.Contains("daily")) return 1;
            else if (str.Contains("week")) return 2;
            else if (str.Contains("month")) return 3;
            else if (str.Contains("year")) return 4;
            else return 5;
        }
    }
};