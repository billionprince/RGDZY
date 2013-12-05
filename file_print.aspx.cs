using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using RGDZY.control;

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
                return;
            }  
        }
    }
}