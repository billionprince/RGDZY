using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using RGDZY.control;

namespace RGDZY
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // TODO: Daniel: Note we should use Web.config to set session's timeout later
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
                if (url == null)
                    Response.Redirect("error.aspx");
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
                    else if (url.Contains("seminar_record"))
                    {
                        if ((ar & Authority.A_PROJECT) == 0x0)
                            Response.Redirect("error.aspx?sc=na");
                    }
                    else if (url.Contains("svnftp_account"))
                    {
                        if ((ar & Authority.A_ACCOUNT) == 0x0)
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
    }
}