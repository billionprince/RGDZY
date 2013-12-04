using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using RGDZY.control;
using System.Data.SqlClient;

namespace RGDZY
{
    public partial class file_print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST" && Request["A"] != null)
            {
                Virtual_Printer.printAllTheFiles();
            }
            if (Request.HttpMethod == "POST" && Request.Files["files[]"]!=null && Request.Files["files[]"].ContentLength > 0)
            {
				string pathstr = Server.MapPath("~/tempfiles/");
				if(!Directory.Exists(pathstr))
				{
					Directory.CreateDirectory(pathstr);
				}
                string filePath = pathstr + Path.GetFileName(Request.Files["files[]"].FileName);
                Request.Files["files[]"].SaveAs(filePath);

                Virtual_Printer.addFile(filePath);
				//TODO: change username and sql connection 
                PrinterLog printerLog = new PrinterLog
                {
					Id = 0;
                    PrintTime = System.DateTime.Now.ToString(),
                    UserName="Admin",//need to coordinate with login module
                    FileName = Path.GetFileName(Request.Files["files[]"].FileName)
                };
				try
				{
					using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
					{
						using(DataContext dc = new DataContext(conn))
						{
							dc.GetTable<PrinterLog>().InsertOnSubmit(printerLog);
							dc.SubmitChanges();
						}
					}
				}
				catch(Exception e)
				{
					//TODO:
				}

                Response.ContentType = "application/json";
                var statuses = new List<FilesStatus>();
                FilesStatus status = new FilesStatus(Request.Files["files[]"].FileName, Request.Files["files[]"].ContentLength, filePath);
                statuses.Add(status);
                JavaScriptSerializer js = new JavaScriptSerializer();
                var jsonObj = js.Serialize(statuses.ToArray());
                Response.Write(jsonObj);
                Response.End();
                return;
            }  
        }
    }
}