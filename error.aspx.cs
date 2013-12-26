using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RGDZY
{
    public partial class error : System.Web.UI.Page
    {
        string info = "Error";

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //Response.TrySkipIisCustomErrors = true;
            //Response.StatusCode = 404;
            //Response.StatusDescription = "404 Page Not Found";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // The following SC assignment should not be set if using HttpErrors in Web.config
            // But must be set if using CustomErrors (to return the correct SC in IIS?)
            /*Response.StatusCode = 404;
             *Response.StatusDescription = "Not found";
             *Response.TrySkipIisCustomErrors = true;
             *Response.Status = "404 Not Found";
            */
            if (Request.QueryString["sc"] != null)
            {
                string tmp = Request.QueryString["sc"];
                switch (tmp)
                {
                    case "401":
                    case "403":
                    case "404":
                    case "411":
                    case "500":
                    case "501":
                        info = Request.QueryString["sc"];
                        break;
                    case "na":
                        info = "No Auth";
                        break;
                    default:
                        break;
                }
            }
        }

        public string getInfo()
        {
            return info;
        }
    }
}