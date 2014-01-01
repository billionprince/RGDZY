using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RGDZY.control
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string getUsername()
        {
            return Authority.getUsername();
        }

        public uint getAuthority()
        {
            return Authority.getAuthority();
        }
    }
}