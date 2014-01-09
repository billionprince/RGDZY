using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RGDZY.control;
using System.Data.Linq;
using System.Data.SqlClient;

using System.Security.Cryptography; // for logo rename hashing
using System.Drawing.Imaging;
using System.Drawing;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for project
    /// </summary>
    public class project : IHttpHandler
    {

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
            context.Response.ContentType = "text/plain";
            context.Response.Write("Error");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void save_milestone_logo(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HttpPostedFile file_logo = context.Request.Files["logo-input"];
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            string responseStr;
            bool default_logo = false;
            if (file_logo == null || string.IsNullOrEmpty(file_logo.FileName) == true)
            {
                default_logo = true;
                /*responseStr = "no logo uploading";
                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(responseStr));
                context.Response.End();
                return;*/
            }
            // save the original picture
            //if (file_avatar != null && string.IsNullOrEmpty(file_avatar.FileName) == false)
            //    file_avatar.SaveAs(context.Server.MapPath("~/App_Data/") + file_avatar.FileName);

            
            // Daniel: This create if not exist is useful for the entire user data, so maybe need to take out as a single module later
            bool isExists = System.IO.Directory.Exists(context.Server.MapPath("~/project_data"));

            if (!isExists)
                System.IO.Directory.CreateDirectory(context.Server.MapPath("~/project_data"));

            string savePath;
            string defaultPath = "~\\project_data\\defaultlogo.jpg";
            if (default_logo)
            {
                savePath = defaultPath; // convert to jpg
                try
                {
                    if (!System.IO.File.Exists(context.Server.MapPath(defaultPath)))
                    {
                        System.IO.File.Copy(context.Server.MapPath("~/media/image/milestone.jpg"),
                            context.Server.MapPath(savePath), false);
                    }
                }
                catch (Exception e)
                {
                    context.Response.Write(e.Message);
                    context.Response.End();
                    return;
                }
            }
            else
            {
                string fileName = file_logo.FileName;
                byte[] dbytearr = System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString() + fileName);
                md5.ComputeHash(dbytearr);

                savePath = "~\\project_data\\" + Convert.ToBase64String(md5.Hash) + ".jpg"; // convert to jpg
                //Resize Image
                System.Drawing.Bitmap bmp = null;
                Graphics canvas;
                int orig_w, orig_h, max_w = 600, max_h = 480;
                double ratio, ratio_crop;
                // according to bootstrap-fileupload-avatar.js; style of image: max-width: 600px; max-height: 480px;
                try
                {
                    bmp = new Bitmap(file_logo.InputStream);
                    canvas = Graphics.FromImage(bmp);
                    canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    orig_h = bmp.Height;
                    orig_w = bmp.Width;

                    Bitmap bmpNew = new Bitmap(400, 400);
                    canvas = Graphics.FromImage(bmpNew);
                    canvas.DrawImage(bmp, new Rectangle(0, 0,
                        400, 400), 0, 0, bmp.Width, bmp.Height,
                    GraphicsUnit.Pixel);
                    bmp = bmpNew;
                }
                catch (Exception e)
                {
                    context.Response.Write(e.Message);
                    return;
                }

                //System.Drawing.Bitmap bmp = null;
                try
                {
                    bmp = new Bitmap(file_logo.InputStream);
                }
                catch (Exception e)
                {
                    context.Response.Write(e.Message);
                    context.Response.End();
                    return;
                }
                string directSavePath = context.Server.MapPath(savePath);
                bmp.Save(directSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            
            string pid = context.Request["project_id"];
            string mid = context.Request["id"];

            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            try
            {
                Table<Milestone> mtable = dc.GetTable<Milestone>();
                var query = from ms in mtable
                            where ms.Id == int.Parse(mid)
                            select ms;

                Milestone minfo = query.FirstOrDefault();
                if (minfo != null && (savePath != defaultPath || string.IsNullOrEmpty(minfo.ImagePath))) // update if save path is specified
                {
                    minfo.ImagePath = savePath;
                    dc.SubmitChanges();
                }
            }
            catch (System.Exception ex)
            {
                string msg = "Error uploading milestone's image...\nReason:";
                msg += ex.Message;
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
            responseStr = "logo upload succeeded!";
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(responseStr));
            context.Response.End();
        }

        public void add_milestone_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int projectid = int.Parse(context.Request["project_id"]);
                var table = dc.GetTable<Milestone>();
                Milestone myinfo = new Milestone();
                myinfo.Id = 0;
                myinfo.Time = DateTime.Now;
                myinfo.Description = context.Request["description"];
                myinfo.ProjectId = projectid;
                table.InsertOnSubmit(myinfo);
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing add_milestone_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void edit_milestone_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int projectid = int.Parse(context.Request["project_id"]);
                string idstr = context.Request["id"].ToString();
                int id = int.Parse(idstr);
                var query = from u in dc.GetTable<Milestone>()
                            where u.Id == id && u.ProjectId == projectid
                            select u;
                var myinfo = query.First();
                myinfo.Description = context.Request["description"];
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_milestone_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void delete_milestone_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int projectid = int.Parse(context.Request["project_id"]);
                int id = int.Parse(context.Request["id"]);
                var table = dc.GetTable<Milestone>();
                var x = table.First(c => c.Id == id && c.ProjectId == projectid);
                table.DeleteOnSubmit(x);
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(null);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing delete_milestone_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void get_milestone_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int projectid = int.Parse(context.Request["project_id"]);
                var query = from u in dc.GetTable<Milestone>()
                            where u.ProjectId == projectid
                            select u;
                foreach (var obj in query)
                {
                    Dictionary<string, object> evt = new Dictionary<string, object>();
                    evt.Add("Id", obj.Id.ToString());
                    evt.Add("Description", obj.Description);
                    rec.Add(evt);
                }

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(rec));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_milestone_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void get_project_detail(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Project_chat> table_project_char = dc.GetTable<Project_chat>();
                        Table<User> table_user = dc.GetTable<User>();
                        int project_id = int.Parse(context.Request["id"]);
                        //var query = from r1 in table_project_char from r2 in table_user 
                        //            where r1.owner == r2.RealName && r1.project_id == project_id 
                        //            select new {
                        //                owner = r1.owner,
                        //                content = r1.chat_content,
                        //                time = r1.chat_time,
                        //                image = r2.Link 
                        //            };
                        var query = from r in table_project_char where r.project_id == project_id select r;
                        foreach (var obj in query)
                        {
                            Dictionary<string, object> evt = new Dictionary<string, object>();
                            evt.Add("name", obj.owner);
                            //evt.Add("content", obj.content);
                            //evt.Add("time", obj.time);
                            //evt.Add("image", obj.image);
                            evt.Add("content", obj.chat_content);
                            evt.Add("time", obj.chat_time);
                            evt.Add("image", "user_data/"+obj.owner+"/a_" + obj.owner + ".jpg");
                            rec.Add(evt);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
        }

        public void put_project_detail_chat(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                using (DataContext dc = new DataContext(conn))
                {
                    try
                    {
                        Table<Project_chat> table_project_char = dc.GetTable<Project_chat>();
                        Project_chat obj = new Project_chat();
                        obj.project_id = int.Parse(context.Request["project_id"]);
                        obj.owner = context.Request["name"];
                        obj.chat_time = context.Request["time"];
                        obj.chat_content = context.Request["content"];
                        table_project_char.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
        }
    }
}