using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using RGDZY.control;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Linq;
using System.Data.SqlClient;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for file_print
    /// </summary>
    public class file_print : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string flag = context.Request["flag"];
            if (flag != null)
            {
                insertfile(HttpContext.Current.Session["_Login_Name"].ToString(), flag);
                Virtual_Printer.set_duplex_printing(Virtual_Printer.GetDefaultPrinter(), flag != "true" ? true : false);
                Virtual_Printer.printAllTheFiles();
            }
            if (context.Request.Files["files[]"] != null && context.Request.Files["files[]"].ContentLength > 0)
            {
                string filename = context.Request.Files["files[]"].FileName;
                addfile(HttpContext.Current.Session["_Login_Name"].ToString(), filename);
                string tempFileDirectoryPath = HttpContext.Current.Server.MapPath("~/tempfiles/");
                if (!Directory.Exists(tempFileDirectoryPath))
                    Directory.CreateDirectory(tempFileDirectoryPath);

                string filePath = HttpContext.Current.Server.MapPath("~/tempfiles/") + Path.GetFileName(filename);
                context.Request.Files["files[]"].SaveAs(filePath);

                Virtual_Printer.addFile(filePath);
                context.Response.ContentType = "application/json";
                var statuses = new List<FilesStatus>();
                FilesStatus status = new FilesStatus(filename, context.Request.Files["files[]"].ContentLength, filePath);
                statuses.Add(status);
                JavaScriptSerializer js = new JavaScriptSerializer();
                var jsonObj = js.Serialize(statuses.ToArray());
                context.Response.Write(jsonObj);
                context.Response.End();
            } 
            context.Response.ContentType = "text/plain";
            context.Response.Write("Success");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void addfile(string name, string filename)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    Table<PrintFile> table_printfile = dc.GetTable<PrintFile>();
                    try
                    {
                        PrintFile evt = new PrintFile();
                        evt.username = name;
                        evt.filename = filename;
                        evt.time = DateTime.Now.ToString();
                        evt.single = -1;
                        table_printfile.InsertOnSubmit(evt);
                        dc.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private void insertfile(string name, string flag = "true")
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    Table<PrintFile> table_printfile = dc.GetTable<PrintFile>();
                    try
                    {
                        var query = from r in table_printfile.Where(e => e.username == name && e.single == -1) select r;
                        foreach (var obj in query)
                        {
                            obj.single = flag == "true" ? 1 : 0;
                        }
                        dc.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
    }
}