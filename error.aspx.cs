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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.StatusCode = 404;
            Response.StatusDescription = "Not found";
            Response.TrySkipIisCustomErrors = true;
            Response.Status = "404 Not Found";

            if (Request.QueryString["sc"] != null)
            {
                string tmp = Request.QueryString["sc"];
                switch (tmp)
                {
                    case "404":
                    case "500":
                        info = Request.QueryString["sc"];
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