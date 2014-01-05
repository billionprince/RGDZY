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
        string[] color = { "yellow", "blue", "red", "green", "gray", "purple" };
        string[] icon = { "icon-trophy", "icon-comments", "icon-facetime-video", "icon-music", "icon-rss", "icon-time", "icon-bar-chart" };

        public DateTime datetime;
        public string date;
        public string time;
        public string title;
        public string text;
        //0 award
        //1 publication
        //2 what else? everyone?
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
//          <img class="timeline-img pull-left" src="media/image/2.jpg" alt="">
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
                        rec.Add(t);
                    }
                }
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
                        rec.Add(t);
                    }
                }
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