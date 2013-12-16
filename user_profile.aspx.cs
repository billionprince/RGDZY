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
            if (Request.HttpMethod == "POST")
            {
                /*Virtual_Printer.printAllTheFiles();*/

            }
            if (Request.HttpMethod == "POST" && Request.Files["files[]"]!=null && Request.Files["files[]"].ContentLength > 0)
            {
                /*
                string tempFileDirectoryPath = Server.MapPath("~/tempfiles/");
                if (!Directory.Exists(tempFileDirectoryPath))
                    Directory.CreateDirectory(tempFileDirectoryPath);

                string filePath = Server.MapPath("~/tempfiles/") + Path.GetFileName(Request.Files["files[]"].FileName);
                Request.Files["files[]"].SaveAs(filePath);

                Virtual_Printer.addFile(filePath);
                

                Response.ContentType = "application/json";
                var statuses = new List<FilesStatus>();
                FilesStatus status = new FilesStatus(Request.Files["files[]"].FileName, Request.Files["files[]"].ContentLength, filePath);
                statuses.Add(status);
                JavaScriptSerializer js = new JavaScriptSerializer();
                var jsonObj = js.Serialize(statuses.ToArray());
                Response.Write(jsonObj);
                Response.End();
                return;*/
            }
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
            //get_my_profile("test");
            if (Session["_Login_Name"] != null)
            {
                get_my_profile(Session["_Login_Name"].ToString());
            }
        }
    }
}