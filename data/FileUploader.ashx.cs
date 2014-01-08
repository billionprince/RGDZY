using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RGDZY.control;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace RGDZY.data
{
    /// <summary>
    /// file_uploader 的摘要说明
    /// </summary>
    public class FileUploader : IHttpHandler
    {
        static string directory = "~/projectfiles/";

//        static Dictionary<string, FilesStatus> statuses = new Dictionary<string, FilesStatus>();

        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["command"];
            if (command != null)
            {
                System.Reflection.MethodInfo method = this.GetType().GetMethod(command);
                if (method != null)
                {
                    method.Invoke(this, new object[] { context });
                    return;
                }
            }
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            context.Response.Write("error");
        }

        public void downloadFile(HttpContext context)
        {
//             string fileName = context.Request["fileName"];
//             string uploadDirectory = context.Server.MapPath(directory);
// 
//             if (String.IsNullOrEmpty(fileName))
//             {
//                 context.Response.ContentType = "text/plain";
//                 context.Response.Write("error");
//                 return;
//             }
//             string filePath = uploadDirectory + fileName;
// 
//             if (File.Exists(filePath))
//             {
//                 context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + statuses[fileName].origin_name + "\"");
//                 context.Response.ContentType = "application/octet-stream";
//                 context.Response.ClearContent();
//                 context.Response.WriteFile(filePath);
//             }
//             else
//             {
//                 context.Response.StatusCode = 404;
//             }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string fileName = context.Request["fileName"];
                string uploadDirectory = context.Server.MapPath(directory);

                if (String.IsNullOrEmpty(fileName))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("error");
                    return;
                }
                string filePath = uploadDirectory + fileName;
                var tablef = dc.GetTable<FileStatus>();
                var o = tablef.First(f => f.Name == fileName);
                if (o == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + o.OriginName + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing downloadFile:";
                msg += ex.Message;
                context.Response.StatusCode = 403;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void removeFile(HttpContext context)
        {
//             string fileName = context.Request["fileName"];
//             string uploadDirectory = context.Server.MapPath(directory);
// 
//             if (!String.IsNullOrEmpty(fileName))
//             {
//                 var filePath = Path.Combine(uploadDirectory, fileName);
//                 if (File.Exists(filePath))
//                 {
//                     File.Delete(filePath);
//                 }
//                 statuses.Remove(fileName);
//             }
// 
// 
//             context.Response.ContentType = "text/plain";
//             context.Response.Write("success");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string fileName = context.Request["fileName"];
                string uploadDirectory = context.Server.MapPath(directory);
                int id = int.Parse(context.Request["id"]);
                var tablef = dc.GetTable<FileStatus>();
                var f = tablef.First(o => o.Name == fileName);
                var tablep = dc.GetTable<ProjectFile>();
                var p = tablep.First(o => o.ProjectId == id && o.FileId == f.Id);
                tablef.DeleteOnSubmit(f);
                tablep.DeleteOnSubmit(p);
                dc.SubmitChanges();
                var filePath = Path.Combine(uploadDirectory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing removeFile:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void getAllFiles(HttpContext context)
        {
//             context.Response.ContentType = "application/json";
//             JavaScriptSerializer js = new JavaScriptSerializer();
//             var jsonObj = js.Serialize(statuses);
//             context.Response.Write(jsonObj);
//             return;
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int id = int.Parse(context.Request["id"]);
                var tablef = dc.GetTable<FileStatus>();
                var tablep = dc.GetTable<ProjectFile>();
                Dictionary<string, FileStatus> statuses = new Dictionary<string, FileStatus>();
//                var query = from f in tablef
//                            join p in tablep.Where(o => o.ProjectId == id)
//                            on f.Id equals p.FileId
//                            into pf
//                            select f;
//                foreach (var o in query)
//                {
//                    statuses.Add(o.Name, o);
//                }
                var query = from p in tablep
                            where p.ProjectId == id
                            select p;
                foreach (var p in query)
                {
                    var f = tablef.First(o => o.Id == p.FileId);
                    if (f != null)
                    {
                        statuses.Add(f.Name, f);
                    }
                }
                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(statuses));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing getAllFiles:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void uploadFile(HttpContext context)
        {
//             string uploadDirectory = context.Server.MapPath(directory);
// 
//             if (!Directory.Exists(uploadDirectory))
//                 Directory.CreateDirectory(uploadDirectory);
// 
//             string timeString = DateTime.Now.ToFileTime().ToString();
//             string originName = Path.GetFileName(context.Request.Files["file"].FileName);
//             string fileName = Path.GetFileNameWithoutExtension(context.Request.Files["file"].FileName)+timeString 
//                 + Path.GetExtension(context.Request.Files["file"].FileName);
//    
//             string filePath = uploadDirectory + fileName;
//             context.Request.Files["file"].SaveAs(filePath);
// 
//             context.Response.ContentType = "application/json";
// 
//             FilesStatus status = new FilesStatus(fileName, context.Request.Files["file"].ContentLength, filePath);
//             status.origin_name = originName;
//             statuses[fileName] = status;
//             //FileStatus file = new FileStatus(0, fileName, originName, "", Files["file"].ContentLength, )
// 
//             JavaScriptSerializer js = new JavaScriptSerializer();
//             var jsonObj = js.Serialize(status);
//             context.Response.Write(jsonObj);
//             return;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string uploadDirectory = context.Server.MapPath(directory);
                if (!Directory.Exists(uploadDirectory))
                    Directory.CreateDirectory(uploadDirectory);
                string timeString = DateTime.Now.ToFileTime().ToString();
                string originName = Path.GetFileName(context.Request.Files["file"].FileName);
                string fileName = Path.GetFileNameWithoutExtension(context.Request.Files["file"].FileName) + timeString
                    + Path.GetExtension(context.Request.Files["file"].FileName);
                string filePath = uploadDirectory + fileName;
                context.Request.Files["file"].SaveAs(filePath);
                var tablef = dc.GetTable<FileStatus>();
                FileStatus f = new FileStatus(fileName,
                    originName,
                    Path.GetExtension(context.Request.Files["file"].FileName),
                    context.Request.Files["file"].ContentLength,
                    "");
                f.FilePath = filePath;
                tablef.InsertOnSubmit(f);
                dc.SubmitChanges();
                context.Response.ContentType = "application/json";
                context.Response.Write(jss.Serialize(f));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing uploadFile:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void uploadThumbnail(HttpContext context)
        {
//             string fileName = context.Request["fileName"];
// 
//             if (!String.IsNullOrEmpty(fileName))
//                 statuses[fileName].thumbnail_url = context.Request["thumbnail"]; 
// 
//             context.Response.ContentType = "text/plain";
//             context.Response.Write("success");
//             return;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string idstring = context.Request["id"];
                int id = int.Parse(idstring);
                var tablef = dc.GetTable<FileStatus>();
                string fileName = context.Request["fileName"];
                var o = tablef.First(f => f.Name == fileName);
                o.ThumbnailUrl = context.Request["thumbnail"];
                var tablep = dc.GetTable<ProjectFile>();
                var p = new ProjectFile();
                p.ProjectId = id;
                p.FileId = o.Id;
                tablep.InsertOnSubmit(p);
                dc.SubmitChanges();
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing uploadThumbnail:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}