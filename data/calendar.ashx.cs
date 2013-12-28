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
        private static string[] backgroundColor = { "green", "yellow", "purple", "red", "grey" };
        private static string[] typelist = { "Once", "Daily", "Weekly", "Monthly", "Yearly", "Personal" };
        private static Dictionary<string, string> mon = new Dictionary<string, string> { { "January", "01" }, { "February", "02" }, { "March", "03" }, { "April", "04" }, { "May", "05" }, { "June", "06" }, { "July", "07" }, { "August", "08" }, { "September", "09" }, { "October", "10" }, { "November", "11" }, { "December", "12" } };
        private static Dictionary<string, string> wek = new Dictionary<string, string> { { "0", "Monday" }, { "1", "Tuesday" }, { "2", "Wednesday" }, { "3", "Thursday" }, { "4", "Friday" }, { "5", "Saturday" }, { "6", "Sunday" } };
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
                        string name = context.Request["name"];
                        //string a = Authority.getUsername();
                        string gpname = (from r in table_usergroup where r.Username == name select r.Groupname).First();
                        var eventlist = from r in table_calendar where r.Participant.Contains(gpname) || r.Participant == "All members" || r.Participant.Contains(name) select r;
                        foreach(var obj in eventlist) 
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            if (obj.Type == 0)
                            {
                                evt.Add("title", obj.Title);
                                evt.Add("allDay", obj.Allday);
                                if (obj.Allday == 0)
                                {
                                    evt.Add("start", obj.Start);
                                    evt.Add("end", obj.End);
                                }
                                evt.Add("backgroundColor", backgroundColor[obj.Type]);
                                if (obj.Url != null && obj.Url.Length != 0)
                                {
                                    evt.Add("url", obj.Url);
                                }
                            }
                            else
                            {
                                rec.AddRange(repeat_event(obj, 1));
                            }
                            rec.Add(evt);
                        }
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
                        var query = from r in table_calendar.Where(e => e.Type != 5) select r;
                        foreach (var obj in query)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.Title);
                            evt.Add("type", typelist[obj.Type]);
                            evt.Add("start", show_time(obj.Start, obj.Type, obj.Allday));
                            evt.Add("end", show_time(obj.End, obj.Type, obj.Allday));
                            evt.Add("group", obj.Participant);
                            evt.Add("detail", obj.Detail);
                            evt.Add("id", obj.Id);
                            evt.Add("allday", obj.Allday);
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
                        obj.Type = type;
                        obj.Title = context.Request["name"].Length == 0 ? null : context.Request["name"];
                        obj.Detail = context.Request["detail"].Length == 0 ? null : context.Request["detail"];
                        obj.Allday = context.Request["allday"] == "false" ? 0 : 1;
                        if (obj.Allday == 0 && obj.Type == 0)
                        {
                            obj.Start = Convert_time_type(context.Request["start"], type);
                            obj.End = Convert_time_type(context.Request["end"], type);
                        }
                        else
                        {
                            obj.Start = context.Request["start"];
                            obj.End = context.Request["end"];
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
                                    if (userlist.Count() == 0) break;
                                    if (gulist.All(e => userlist.Contains(e)))
                                    {
                                        res.Add(groupname);
                                        userlist = userlist.Except(gulist).ToList();
                                    }
                                }
                                res.AddRange(userlist);
                                obj.Participant = string.Join(",", res);
                            }
                        }
                        obj.Sendemail = 0;
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

        public void delete_calendar_by_id(HttpContext context)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
                {
                    using (DataContext dc = new DataContext(conn))
                    {
                        Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                        Guid id = new Guid(context.Request["id"]);
                        var query = from r in table_calendar where r.Id == id select r;
                        table_calendar.DeleteAllOnSubmit(query);
                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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

        private string Convert_time_type(string str, int type, string month=null, string week=null, string day=null)
        {
            string rec = "";
            List<string> lst = str.Split(' ').ToList();
            if (type == 0)
            {
                //"22 November 2013 - 19:30"
                //2009-11-05T13:15:30Z
                string d = lst[0];
                string m = mon[lst[1]];
                string y = lst[2];
                string t = lst[4];
                rec = y + "-" + m + "-" + d + "T" + t + ":00Z";
            }
            return rec;
        }
        
        private string show_time(string str, int type, int allday)
        {
            string rec = null;
            if (type == 0)
            {
                if (allday == 0)
                {
                    rec = str.Replace("T", " ");
                    rec = rec.Substring(0, rec.Length - 4);
                }
                else
                {
                    rec = "null";
                }
                return rec;
            }
            List<string> lst = str.Split(' ').ToList();
            if (type == 1)
            {
                rec = "Everyday " + lst[0];
            }
            else if (type == 2)
            {
                rec = "Every " + wek[lst[0]] + " ";
                if (allday == 1)
                {
                    rec += lst[1];
                }
            }
            else if (type == 3)
            {
                switch (lst[0])
                {
                    case "0":
                        rec = "Every 1st ";
                        break;
                    case "1":
                        rec = "Every 2nd ";
                        break;
                    case "2":
                        rec = "Every 3rd ";
                        break;
                    default:
                        rec = "Every " + lst[0] + "th ";
                        break;
                }
                if (allday == 0)
                {
                    rec += lst[1];
                }
            }
            else if (type == 4)
            {
                rec = "Every " + mon.Where(e => e.Value == (lst[0].Length == 1 ?"0"+lst[0]:lst[0])).Select(e => e.Key).First() + " ";
                switch (lst[1])
                {
                    case "0":
                        rec += "1st ";
                        break;
                    case "1":
                        rec += "2nd ";
                        break;
                    case "2":
                        rec += "3rd ";
                        break;
                    default:
                        rec += lst[1] + "th ";
                        break;
                }
                if (allday == 0) rec += lst[2];
            }
            return rec;
        }

        /// <summary>
        /// repeat event if type > 0 
        /// </summary>
        /// <param name="obj">event</param>
        /// <param name="scope">repeat time scope</param>
        /// <returns></returns>
        private List<Dictionary<string, object>> repeat_event(Calendar obj, int scope)
        {
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DateTime ed = DateTime.Now;
            if (scope == 1)
            {
                ed = new DateTime(DateTime.Now.Year, 12, 31);
            }
            switch (obj.Type)
            {
                case 1:
                    DateTime st = DateTime.Now;
                    int step = 1;
                    while (st <= ed)
                    {
                        Dictionary<string, object> evt = new Dictionary<string, object>();
                        evt.Add("title", obj.Title);
                        evt.Add("allDay", obj.Allday);
                        if (obj.Allday == 0)
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd")+"T"+obj.Start+"Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd")+"T"+obj.End+"Z");
                        }
                        else
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                        }
                        evt.Add("backgroundColor", backgroundColor[obj.Type]);
                        if (obj.Url != null && obj.Url.Length != 0)
                        {
                            evt.Add("url", obj.Url);
                        }
                        st = st.AddDays(step);
                        rec.Add(evt);
                    }
                    break;
                case 2:
                    List<string> st_lst = obj.Start.Split(' ').ToList();
                    List<string> ed_lst = obj.End.Split(' ').ToList();
                    st = DateTime.Now.StartOfWeek(DayOfWeek.Monday+int.Parse(st_lst[0]));
                    step = 7;
                    while (st <= ed)
                    {
                        Dictionary<string, object> evt = new Dictionary<string, object>();
                        evt.Add("title", obj.Title);
                        evt.Add("allDay", obj.Allday);
                        if (obj.Allday == 0)
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + st_lst[1] + "Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + ed_lst[1] + "Z");
                        }
                        else
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                        }
                        evt.Add("backgroundColor", backgroundColor[obj.Type]);
                        if (obj.Url != null && obj.Url.Length != 0)
                        {
                            evt.Add("url", obj.Url);
                        }
                        st = st.AddDays(step);
                        rec.Add(evt);
                    }
                    break;
                case 3:
                    st_lst = obj.Start.Split(' ').ToList();
                    ed_lst = obj.End.Split(' ').ToList();
                    if (DateTime.Now.Day > int.Parse(st_lst[0]))
                    {
                        st = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, int.Parse(st_lst[0]));
                    }
                    else
                    {
                        st = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(st_lst[0]));
                    }
                    step = 1;
                    while (st <= ed)
                    {
                        Dictionary<string, object> evt = new Dictionary<string, object>();
                        evt.Add("title", obj.Title);
                        evt.Add("allDay", obj.Allday);
                        if (obj.Allday == 0)
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + st_lst[1] + "Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd")+"T"+ed_lst[1]+"Z");
                        }
                        else
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                        }
                        evt.Add("backgroundColor", backgroundColor[obj.Type]);
                        if (obj.Url != null && obj.Url.Length != 0)
                        {
                            evt.Add("url", obj.Url);
                        }
                        st = st.AddMonths(step);
                        rec.Add(evt);
                    }
                    break;
                case 4:
                    st_lst = obj.Start.Split(' ').ToList();
                    ed_lst = obj.End.Split(' ').ToList();
                    st = new DateTime(DateTime.Now.Year, int.Parse(st_lst[0]), int.Parse(st_lst[1]));
                    step = 1;
                    while (st <= ed)
                    {
                        Dictionary<string, object> evt = new Dictionary<string, object>();
                        evt.Add("title", obj.Title);
                        evt.Add("allDay", obj.Allday);
                        if (obj.Allday == 0)
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + st_lst[2] + "Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + ed_lst[2] + "Z");
                        }
                        else
                        {
                            evt.Add("start", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                            evt.Add("end", st.ToString("yyyy-MM-dd") + "T" + "00:00:00Z");
                        }
                        evt.Add("backgroundColor", backgroundColor[obj.Type]);
                        if (obj.Url != null && obj.Url.Length != 0)
                        {
                            evt.Add("url", obj.Url);
                        }
                        st = st.AddYears(step);
                        rec.Add(evt);
                    }
                    break;
            }
            return rec;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }

}