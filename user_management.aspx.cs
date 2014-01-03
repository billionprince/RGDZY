using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Linq;
using RGDZY.control;

namespace RGDZY
{
    public partial class user_management : System.Web.UI.Page
    {
        public List<User> userlist;

        public List<User> get_userlist()
        {
            return userlist;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            get_all_profile();
        }

        public void get_all_profile()
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            var query = from u in dc.GetTable<User>()
                        select u;
            userlist = query.ToList();

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }
    }
}