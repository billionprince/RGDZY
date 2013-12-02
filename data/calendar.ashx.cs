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
    /// Summary description for get_user_calendar
    /// </summary>
    public class calendar : IHttpHandler
    {
        private static string[] backgroundColor = { "yellow", "green", "purple", "red", "grey" };
        private static string[] typelist = { "Once", "Daily", "Weekly", "Monthly", "Yearly", "Personal" };
        private enum Response { success, fail};

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

        public void get_user_calendar(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn)) 
                {
                    Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                    Table<UserGroup> table_usergroup = dc.GetTable<UserGroup>();
                    try
                    {
                        //var q = from r in table_calendar join table_usergroup
                        //List<Calendar> query = (from r in table_calendar.Where(e => ) select r).ToList();
                        //foreach (var obj in query)
                        //{
                        //    rec.Add(Calendar_to_dict(obj));
                        //}
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
        }

        public void get_calendar_list(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn)) 
                {
                    try
                    {
                        Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                        var query = from r in table_calendar.Where(e => e.Type != 4) select r;
                        foreach (var obj in query)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.Title);
                            evt.Add("type", typelist[obj.Type]);
                            evt.Add("start", obj.Start);
                            evt.Add("end", obj.End);
                            evt.Add("group", obj.Participant);
                            evt.Add("detail", obj.Detail);
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

        public void put_calendar_event(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                    Table<UserGroup> table_group = dc.GetTable<UserGroup>();
                    Table<User> table_user = dc.GetTable<User>();
                    try
                    {
                        int type = int.Parse(context.Request["type"]);
                        Calendar obj = new Calendar();
                        obj.Id = Guid.NewGuid();
                        obj.Title = context.Request["name"].Length == 0 ? null : context.Request["name"];
                        obj.Detail = context.Request["detail"].Length == 0 ? null : context.Request["detail"];
                        obj.Allday = context.Request["allday"] == "false" ? 0 : 1;
                        if (obj.Allday == 0)
                        {
                            obj.Start = Convert_time_type(context.Request["start"]);
                            obj.End = Convert_time_type(context.Request["end"]);
                        }
                        else
                        {
                            obj.Start = obj.End = null;
                        }
                        obj.Creator = context.Request["creator"];
                        obj.Url = null;
                        List<string> userlist = context.Request["user"].Split(',').ToList();
                        {
                            var ulist = (from r in table_user select r.Name).Distinct().ToList();
                            if (ulist.Count() == userlist.Count())
                            {
                                obj.Participant = "All members";
                            }
                            else
                            {
                                var grouplist = (from r in table_group select r.Groupname).Distinct().ToList();
                                List<string> res = new List<string>();
                                foreach (var groupname in grouplist)
                                {
                                    var gulist = (from r in table_group where r.Groupname == groupname select r.Username).ToList();
                                    if (userlist.All(e => gulist.Contains(e)))
                                    {
                                        res.Add(groupname);
                                        userlist = userlist.Except(gulist).ToList();
                                    }
                                }
                                res.AddRange(userlist);
                                obj.Participant = string.Join(",", res);
                            }
                        }
                        table_calendar.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(Response.success);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private Dictionary<string, object> Calendar_to_dict(Calendar obj)
        {
            Dictionary<string, object> evt = new Dictionary<string, object>();
            evt.Add("title", obj.Title);
            evt.Add("start", obj.Start);
            evt.Add("end", obj.End);
            evt.Add("backgroundColor", backgroundColor[obj.Type]);
            evt.Add("allDay", obj.Allday);
            if (obj.Url != null)
            {
                evt.Add("url", obj.Url);
            }
            return evt;
        }

        private string Convert_time_type(string str)
        {
            //"22 November 2013 - 19:30"
            string rec="abc";
            return rec;
        }

    }

}