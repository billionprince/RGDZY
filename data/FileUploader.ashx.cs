using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RGDZY.control;
using System.Web.Script.Serialization;

namespace RGDZY.data
{
    /// <summary>
    /// file_uploader 的摘要说明
    /// </summary>
    public class FileUploader : IHttpHandler
    {
        static string directory = "~/projectfiles/";
        static Dictionary<string, FilesStatus> statuses = new Dictionary<string, FilesStatus>();
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
            string fileName = context.Request["fileName"];
            string uploadDirectory = context.Server.MapPath(directory);

            if (String.IsNullOrEmpty(fileName))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("error");
                return;
            }
            string filePath = uploadDirectory + fileName;

            if (File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + statuses[fileName].origin_name + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        }

        public void removeFile(HttpContext context)
        {
            string fileName = context.Request["fileName"];
            string uploadDirectory = context.Server.MapPath(directory);

            if (!String.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(uploadDirectory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                statuses.Remove(fileName);
            }


            context.Response.ContentType = "text/plain";
            context.Response.Write("success");
        }

        public void getAllFiles(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonObj = js.Serialize(statuses);
            context.Response.Write(jsonObj);
            return;
        }

        public void uploadFile(HttpContext context)
        {
            string uploadDirectory = context.Server.MapPath(directory);

            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            string timeString = DateTime.Now.ToFileTime().ToString();
            string originName = Path.GetFileName(context.Request.Files["file"].FileName);
            string fileName = Path.GetFileNameWithoutExtension(context.Request.Files["file"].FileName)+timeString 
                + Path.GetExtension(context.Request.Files["file"].FileName);
   
            string filePath = uploadDirectory + fileName;
            context.Request.Files["file"].SaveAs(filePath);

            context.Response.ContentType = "application/json";

            FilesStatus status = new FilesStatus(fileName, context.Request.Files["file"].ContentLength, filePath);
            status.origin_name = originName;
            statuses[fileName] = status;
            //FileStatus file = new FileStatus(0, fileName, originName, "", Files["file"].ContentLength, )

            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonObj = js.Serialize(status);
            context.Response.Write(jsonObj);
            return;
        }

        public void uploadThumbnail(HttpContext context)
        {
            string fileName = context.Request["fileName"];

            if (!String.IsNullOrEmpty(fileName))
                statuses[fileName].thumbnail_url = context.Request["thumbnail"]; 

            context.Response.ContentType = "text/plain";
            context.Response.Write("success");
            return;
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