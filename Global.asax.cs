using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace RGDZY
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            /*
            if (HttpContext.Current.Session == null)
            {}
            else if (Session["_Login_Name"] == null )
            {
                string url = Request.Path;
                if (url == null)
                    Response.Redirect("shit1.aspx");
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
             */
                    //(!Request.Path.EndsWith("login.aspx")) &&
                    //(!Request.Path.EndsWith("login.ashx"))
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}