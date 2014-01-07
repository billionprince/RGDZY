using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RGDZY.control;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for project
    /// </summary>
    public class timeline : IHttpHandler, IComparable, IRequiresSessionState
    {
        string[] color = { "red", "blue", "green", "gray", "yellow", "purple" };
        string[] icon = { "icon-trophy", "icon-bar-chart", "icon-time", "icon-comments", "icon-facetime-video", "icon-rss", "icon-music" };

        public DateTime datetime;
        public string date;
        public string time;
        public string title;
        public string text;
        public string img;
        //0 award
        //1 publication
        //2 milestone
        public int type;

        public int CompareTo(object obj)
        {
            if (obj is timeline)
            {
                timeline t = obj as timeline;
                return this.datetime.CompareTo(t.datetime);
            }
            throw new Exception("not a timeline\n");
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        string get_timeline_obj_html(timeline t)
        {
            string temp = "";
            temp += string.Format("<li class=\"timeline-{0}\"><div class=\"timeline-time\">", color[t.type]);
            temp += string.Format("<span class=\"date\">{0}</span><span class=\"time\">{1}</span>", t.time, t.date);
            temp += string.Format("</div><div class=\"timeline-icon\"><i class=\"{0}\"></i></div><div class=\"timeline-body\">", icon[t.type]);
            temp += string.Format("<h2>{0}</h2><div class=\"timeline-content\">", t.title);
            if (!string.IsNullOrEmpty(t.img))
            {
                temp += string.Format("<img class=\"timeline-img pull-left\" src=\"{0}\" alt=\"\">", t.img);
            }
            temp += t.text + "</div>";
// 			<div class="timeline-footer">
// 				<a href="#" class="nav-link pull-right">
// 				Read more <i class="m-icon-swapright m-icon-white"></i>                              
// 				</a>  
// 			</div>
            temp += "</div></li>";
            return temp;
        }

        public void get_all_timeline(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<timeline> rec = new List<timeline>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            string bigstr = "";

            try
            {
                string username = context.Session["_Login_Name"].ToString();
                #region AWARD
                {
                    var query = from o in dc.GetTable<Award>()
                                where o.UserName == username
                                select o;
                    foreach (var o in query)
                    {
                        timeline t = new timeline();
                        t.type = 0;
                        t.datetime = o.Time;
                        t.date = o.Time.ToShortDateString();
                        t.time = o.Time.ToShortTimeString();
                        t.title = "AWARD ACHIEVED!";
                        t.text = string.Format("{0}({1})", o.Name, o.Year);
                        t.img = null;
                        rec.Add(t);
                    }
                }
                #endregion
                #region Publication
                {
                    var query = from o in dc.GetTable<Publication>()
                                where o.UserName == username
                                select o;
                    foreach (var o in query)
                    {
                        timeline t = new timeline();
                        t.type = 1;
                        t.datetime = o.Time;
                        t.date = o.Time.ToShortDateString();
                        t.time = o.Time.ToShortTimeString();
                        t.title = "PAPER PUBLISHED!";
                        t.text = string.Format("{2} {0}'{1}", o.Conference, o.Year, o.PaperName);
                        t.img = null;
                        rec.Add(t);
                    }
                }
                #endregion
                #region Milestone
                {
                    var query1 = from o in dc.GetTable<UserProject>()
                                where o.UserName == username
                                join p in dc.GetTable<Milestone>()
                                on o.ProjectId equals p.ProjectId
                                select p;
                    var query2 = from o in query1
                                 join p in dc.GetTable<Project>()
                                 on o.ProjectId equals p.Id
                                 select new
                                 {
                                     p.FullName,
                                     p.Name,
                                     o.Time,
                                     o.Description,
                                     o.ImagePath
                                 };
                    foreach (var o in query2)
                    {
                        timeline t = new timeline();
                        t.type = 2;
                        t.datetime = o.Time;
                        t.date = o.Time.ToShortDateString();
                        t.time = o.Time.ToShortTimeString();
                        t.title = string.Format("{0}({1}) MILESTONE!", o.FullName, o.Name);
                        t.text = string.Format("{0}", o.Description);
                        t.img = o.ImagePath;
                        rec.Add(t);
                    }
                }
                #endregion

                rec.Sort();
                for (int i = 0; i < rec.Count; i++)
                {
                    bigstr = get_timeline_obj_html(rec[i]) + bigstr;
                }
                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(bigstr));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_all_timeline:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }
    }
}