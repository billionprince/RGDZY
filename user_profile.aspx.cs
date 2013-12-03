using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RGDZY.control;
using System.Web.Script.Serialization;
using System.Data.Linq;

namespace RGDZY
{
    public partial class user_profile : System.Web.UI.Page
    {
        public User myinfo;
        public List<Award> myaward;
        public List<Publication> mypublication;

        public User get_user()
        {
            return myinfo;
        }

        public List<Award> get_award()
        {
            return myaward;
        }

        public List<Publication> get_publication()
        {
            return mypublication;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
                
        public void get_my_profile(string username)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            var query = from u in dc.GetTable<User>()
                        where u.Name == username
                        select u;
            myinfo = query.First();
            var query1 = from a in dc.GetTable<Award>()
                         where a.UserName == username
                         select a;
            myaward = query1.ToList();
            var query2 = from a in dc.GetTable<Publication>()
                         where a.UserName == username
                         select a;
            mypublication = query2.ToList();

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            get_my_profile("test");
//             if (Session["_Login_Name"] != null)
//             {
//                 get_my_profile(Session["_Login_Name"].ToString());
//             }
        }
    }
}