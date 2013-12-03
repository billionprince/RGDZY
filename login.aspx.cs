using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using RGDZY.control;

namespace RGDZY
{
    public partial class login : System.Web.UI.Page//, ICallbackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            HttpRequest hr = Request;
            string isRed = hr.QueryString["redirect"];
             */
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["_Login_Name"] != null)
            {
                Trace.Warn("OnPreInit: Already Logined.");
                NameValueCollection data = new NameValueCollection();
                data.Add("action", "signed");
                //HttpHelper.RedirectAndPOST(this.Page, "default.aspx", data);
                
                Response.Redirect("default.aspx", true); // true == Response.End(); stop page's execution before redirected entirely
                //Response.End();
            }
        }

        /*
        protected override void OnPreInit(EventArgs e)
        {
            if (Session["_Login_Name"] != null)
            {
                Trace.Warn("OnPreInit: Already Logined.");
                NameValueCollection data = new NameValueCollection();
                data.Add("action", "signed");
                HttpHelper.RedirectAndPOST(this.Page, "default.aspx", data);
                //Response.Redirect("default.aspx", false);
            }
            base.OnPreInit(e);
            //if (session.logged_in == false)
            //{
            //    Response.Redirect("login.aspx", false);
            //}
            //
        }
        */
    }
}