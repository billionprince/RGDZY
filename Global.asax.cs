using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using RGDZY.control;
using System.Timers;
using System.Data.Linq;
using System.Data.SqlClient;

namespace RGDZY
{
    public class Global : System.Web.HttpApplication
    {
        // to enable default page (if url is web site's root) if System.webServer is set in Web.config
        // Refer here: blog.tentaclesoftware.com/archive/2011/04/07/asp-net-routing-and-default-aspx.aspx
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.Add("Default", new Route(string.Empty, new RouteHandler("~/Site/Default.aspx")));
            routes.MapPageRoute("Default", string.Empty, "~/login.aspx");
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            // overrides default reedirecting policy of IIS
            RegisterRoutes(RouteTable.Routes);
            Timer t = new Timer(int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailInterval"]));
            t.Elapsed += new ElapsedEventHandler(sendemail);
            t.AutoReset = true;
            t.Enabled = true; 
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Re-triggered by every POST/GET request(?)
            Session.Timeout = 600;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null)
            {}
            else if (Session["_Login_Name"] == null)
            {
                /* Try this to set a fake account w/o connecting DB for validation
                Session["_Login_Name"] = "test";
                Session["_Login_Authority"] = (uint)0x3;
                return;
                */

                string url = Request.Path;

                // could be "/" if whole url is web site's root (see: RegisterRoutes())
                if (url == null)
                    Response.Redirect("login.aspx?action=unknown_state");
                else if (url == "/")
                    Response.Redirect("login.aspx");
                else
                {
                    url = url.Split(new[] { '?' })[0];
                    if ((!url.EndsWith("login.aspx")) &&
                        (!url.EndsWith("login.ashx")))
                    {
                        Response.Redirect("login.aspx?action=redirect");
                    }
                }
            }
            else // check authority
            {
                uint ar = 0x0;
                if( Session["_Login_Authority"] != null )
                    ar = (uint)Session["_Login_Authority"];

                string url = Request.Path;
                if (url == null)
                    Response.Redirect("error.aspx");
                else
                {
                    if (url.Contains("device_list"))
                    {
                        if ((ar & Authority.A_DEVICE) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    else if (url.Contains("page_schedule_setting"))
                    {
                        if ((ar & Authority.A_SCHEDULE) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    /*else if (url.Contains("seminar_record"))
                    {
                        if ((ar & Authority.A_PROJECT) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }*/
                    else if (url.Contains("svnftp_account"))
                    {
                        if ((ar & Authority.A_ACCOUNT) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    else if (url.Contains("user_management") || url.Contains("group_management"))
                    {
                        if ((ar & Authority.A_ADMIN) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    else if (url.Contains("file_record"))
                    {
                        if ((ar & Authority.A_FILE) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    else if (url.Contains("seminar_table"))
                    {
                        if ((ar & Authority.A_SEMINAR) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                }
            }
                    //(!Request.Path.EndsWith("login.aspx")) &&
                    //(!Request.Path.EndsWith("login.ashx"))
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Response.Redirect("login.aspx?action=error");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void sendemail(object sender, ElapsedEventArgs e)
        {

            try
            {
                using (SendEmail obj = new SendEmail())
                {
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
                    {
                        using (DataContext dc = new DataContext(conn))
                        {
                            Table<Calendar> table_calendar = dc.GetTable<Calendar>();
                            Table<User> table_user = dc.GetTable<User>();
                            Table<UserGroup> table_usergroup = dc.GetTable<UserGroup>();
                            var query = from r in table_calendar where r.Type == 2 select r;
                            DateTime tim = DateTime.Now;
                            foreach(var i in query)
                            {
                                if (i.Allday == 1) continue;
                                List<string> lst = i.Start.Split(' ').ToList();
                                if (int.Parse(lst[0]) != (int)tim.DayOfWeek || Convert.ToDateTime(i.Sendemail).Date == DateTime.Today) continue;
                                DateTime tt = Convert.ToDateTime(lst[1]);
                                if ((int)tt.Subtract(tim).TotalHours <= 1)
                                {
                                    List<string> namlst = i.Participant.Split(',').ToList();
                                    List<string> addrlst = new List<string>();
                                    foreach (var j in namlst)
                                    {
                                        if (j == "PA" || j == "TCLOUD" || j == "NETWORK")
                                        {
                                            var q = (from r in table_usergroup from p in table_user where r.Username == p.RealName && r.Groupname == j select p.Email).ToList();
                                            addrlst.AddRange(q);
                                        }
                                        else
                                        {
                                            if (table_user.Any(x => x.RealName == j))
                                            {
                                                var addr = (from r in table_user where r.RealName == j select r.Email).First().ToString();
                                                addrlst.Add(addr);
                                            }
                                        }
                                    }
                                    string addremail = string.Join(",", addrlst.Distinct().ToList());
                                    if (addremail != null && addremail.Length > 0) 
                                    {
                                        obj.send(string.Join(",", addrlst.Distinct().ToList()), i.Title, i.Title + " " + lst[1]);
                                        i.Sendemail = tim.ToString("yyyy-MM-dd");
                                        dc.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}