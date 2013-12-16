using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using System.Web.SessionState;
using System.Data.Linq;
using RGDZY.control;
using System.Web.Script.Serialization;

using System.Drawing.Imaging;
using System.Drawing;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for login
    /// </summary>
    public class login_service : IHttpHandler, IRequiresSessionState
    {
        private string ValidateExec(string username, string password, out uint authority)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            try{
                Table<User> tu = dc.GetTable<User>();
                var query = from u in tu
                            where u.Name == username
                            select u;
                //User user = query.ToList()[0];
                User user = query.FirstOrDefault();

                // found username
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        authority = (uint)user.Authority;
                        return "Success";
                    }
                }
                authority = 0x0;
		return "False";
            }
            catch (System.Exception exMsg)
            {
                string msg = "Error occured while executing:";
                msg += exMsg.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

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

        public void change_password(HttpContext context)
        {
            string oldpwd = context.Request["oldpwd"];
            string newpwd1 = context.Request["newpwd1"];
            string newpwd2 = context.Request["newpwd2"];
            string username = context.Session["_Login_Name"].ToString();
            string resp;
            uint ar;
            context.Response.ContentType = "text/plain";
            resp = ValidateExec(username, oldpwd, out ar);
            if (resp == null)
                resp = "ErrorPassword";
            else if (resp == "Success")
            {
                if (newpwd1 != newpwd2)
                {
                    resp = "PasswordNotMatch";
                }
                else
                {
                    DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
                    try
                    {
                        Table<User> tu = dc.GetTable<User>();
                        var query = from u in tu
                                    where u.Name == username
                                    select u;
                        User user = query.FirstOrDefault();
                        if (user != null)
                        {
                            user.Password = newpwd1;
                        }
                        dc.SubmitChanges();
                        resp = "Password changed successfully";
                    }
                    catch (Exception exMsg)
                    {
                        string msg = "Error occured while executing:";
                        msg += exMsg.Message;
                        resp = msg;
                        throw new Exception(msg);
                    }
                    finally
                    {
                        DBConnectionSingleton.Instance.ReturnDBConnection(dc);
                    }
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(resp));

        }

        public void load_user_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string username = System.Web.HttpContext.Current.Session["_Login_Name"].ToString();
                var query = from u in dc.GetTable<User>()
                            where u.Name == username
                            select u;
                var myinfo = query.First();
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing load_user_settings:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void save_user_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string username = System.Web.HttpContext.Current.Session["_Login_Name"].ToString();
                var query = from u in dc.GetTable<User>()
                            where u.Name == username
                            select u;
                var myinfo = query.First();
                myinfo.RealName = context.Request["RealName"];
                myinfo.StudentId = context.Request["StudentId"];
                myinfo.Email = context.Request["Email"];
                myinfo.Phone = context.Request["Phone"];
                myinfo.Hometown = context.Request["Hometown"];
                myinfo.Link = context.Request["Link"];
                myinfo.Introduction = context.Request["Introduction"];
                try
                {
                    myinfo.Birthday = DateTime.Parse(context.Request["Birthday"].ToString());
                }
                catch
                {

                }
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing save_user_settings:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void save_user_avatar(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HttpPostedFile file_avatar = context.Request.Files["avatar-input"];

            string responseStr;
            if (file_avatar == null || string.IsNullOrEmpty(file_avatar.FileName) == true)
            {
                responseStr = "avatar upload failed!";
                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(responseStr));
                context.Response.End();
                return;
            }

            string fileName = file_avatar.FileName;
            // save the original picture
            //if (file_avatar != null && string.IsNullOrEmpty(file_avatar.FileName) == false)
            //    file_avatar.SaveAs(context.Server.MapPath("~/App_Data/") + file_avatar.FileName);

            string xstr = context.Request.Form["x1"];
            string ystr = context.Request.Form["y1"];
            string wstr = context.Request.Form["w"];
            string hstr = context.Request.Form["h"];
            string picPath = context.Request["pic"];
            string loginName = "N/A";

            string avatar_name;
            if (context.Session["_Login_Name"] == null)
            {
                avatar_name = "l_";
                avatar_name += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff");
                avatar_name += "_";
                avatar_name += new Random().ToString();
            }
            else
            {
                avatar_name = "a_";
                if (string.IsNullOrEmpty(context.Session["_Login_Name"].ToString()) == true)
                    avatar_name += "default";
                else
                {
                    loginName = context.Session["_Login_Name"].ToString();
                    avatar_name += loginName;
                }
            }

            // Daniel: This create if not exist is useful for the entire user data, so maybe need to take out as a single module later
            bool isExists = System.IO.Directory.Exists(context.Server.MapPath("~/user_data/" + loginName));

            if (!isExists)
                System.IO.Directory.CreateDirectory(context.Server.MapPath("~/user_data/" + loginName));

            string savePath = "~/user_data/" + loginName + "/" + avatar_name + ".jpg";
            int x = 0;
            int y = 0;
            int w = 1;
            int h = 1;
            try
            {
                x = int.Parse(xstr);
                y = int.Parse(ystr);
                w = int.Parse(wstr);
                h = int.Parse(hstr);
            }
            catch { }
            System.Drawing.Bitmap bmp = new Bitmap(file_avatar.InputStream);
            Graphics canvas = Graphics.FromImage(bmp);
            try
            {
                Bitmap bmpNew = new Bitmap(w, h);
                canvas = Graphics.FromImage(bmpNew);
                    canvas.DrawImage(bmp, new Rectangle(0, 0,
                    200, 200), x, y, w, h,
                    GraphicsUnit.Pixel);
                bmp = bmpNew;
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }

            string directSavePath = context.Server.MapPath(savePath);
            bmp.Save(directSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            responseStr = "avatar upload succeeded!";
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(responseStr));
            context.Response.End();
        }

        public void get_validate(HttpContext context)
        {
            string un = context.Request["username"];
            string pw = context.Request["password"];
            string resp;
            uint ar;
            context.Response.ContentType = "text/plain";

            resp = ValidateExec(un, pw, out ar);
            if (resp == null)
                resp = "Error";
            else if (resp == "Success")
            {
                context.Session["_Login_Name"] = un;
                context.Session["_Login_Authority"] = ar;
                //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(un, false);
            }
            
            context.Response.Write(resp);
            Console.WriteLine(resp);
        }

        public void profile_update(HttpContext context)
        {
            string un = context.Request["username"];
            string pw = context.Request["password"];
            string resp;
            uint ar;
            context.Response.ContentType = "text/plain";

            resp = ValidateExec(un, pw, out ar);
            if (resp == null)
                resp = "Error";
            else if (resp == "Success")
            {
                context.Session["_Login_Name"] = un;
                context.Session["_Login_Authority"] = ar;
            }

            context.Response.Write(resp);
            Console.WriteLine(resp);
        }

        public void get_logout(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["_Login_Name"] != null)
                context.Session.Remove("_Login_Name");

            if (context.Session["_Login_Authority"] != null)
                context.Session.Remove("_Login_Authority");

            //context.Session.RemoveAll();

            //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //context.Response.Cache.SetNoServerCaching();
            //context.Response.Cache.SetNoStore();

            //System.Web.Security.FormsAuthentication.SignOut();
            context.Response.Redirect("../login.aspx");
            context.Response.End();
        }

        public void get_username(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string username = Authority.getUsername();
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(username));
        }

        public void get_authority(HttpContext context)
        {
            //return Authority.getAuthority();
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
