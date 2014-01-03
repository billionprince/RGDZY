using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;

namespace RGDZY.control
{
    public partial class Menu : System.Web.UI.UserControl
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

        public uint getFlag(string f)
        {
            switch (f)
            {
                case "A_GUEST":
                    return Authority.A_GUEST;
                case "A_NORMAL":
                    return Authority.A_NORMAL;
                case "A_SCHEDULE":
                    return Authority.A_SCHEDULE;
                case "A_DEVICE":
                    return Authority.A_DEVICE;
                case "A_PROJECT":
                    return Authority.A_PROJECT;
                case "A_ACCOUNT":
                    return Authority.A_ACCOUNT;
                case "A_ADMIN":
                    return Authority.A_ADMIN;
                default:
                    return 0x0;
            }
        }
    }
}