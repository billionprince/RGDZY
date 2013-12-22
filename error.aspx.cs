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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.StatusCode = 404;
            Response.StatusDescription = "Not found";
            Response.TrySkipIisCustomErrors = true;
            Response.Status = "404 Not Found";
        }
    }
}