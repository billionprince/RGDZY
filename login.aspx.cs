using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        protected override void OnPreInit(EventArgs e)
        {
            Trace.Warn("OnPreInit");
            /*if (session.logged_in == false)
            {
                Response.Redirect("login.aspx", false);
            }
            */
            base.OnPreInit(e);
        }
    }
}