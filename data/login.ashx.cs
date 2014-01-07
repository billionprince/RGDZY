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
                //dc.SubmitChanges();

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

            System.Drawing.Bitmap bmp = null;
            Graphics canvas;
            int orig_w, orig_h, max_w = 600, max_h = 480;
            double ratio, ratio_crop;
            // according to bootstrap-fileupload-avatar.js; style of image: max-width: 600px; max-height: 480px;
            try
            {
                bmp = new Bitmap(file_avatar.InputStream);
                canvas = Graphics.FromImage(bmp);
                canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                orig_h = bmp.Height;
                orig_w = bmp.Width;

                ratio_crop = (double)h / 200.0d;
                // orig image zoomed
                if (orig_h > max_h || orig_w > max_w)
                {
                    double ratio_h = ((double)orig_h) / ((double)max_h);
                    double ratio_w = ((double)orig_w) / ((double)max_w);
                    ratio = (ratio_h > ratio_w) ? ratio_h : ratio_w;
                    x = (int)(x * ratio);
                    y = (int)(y * ratio);
                    w = (int)(w * ratio);
                    h = (int)(h * ratio); 
                }

                Bitmap bmpNew = new Bitmap(200, 200);
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
            context.Response.Write(jss.Serialize(loginName));
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

        // ---- Start User Management ----
        public void getAllUsers(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<User> res = new List<User>();

            try
            {
                var query = from user in dc.GetTable<User>()
                            select user;

                foreach (var user in query)
                {
                    User userInfo = new User();
                    userInfo.Name = user.Name;
                    userInfo.Authority = user.Authority;
                    userInfo.GroupName = user.GroupName;
                    userInfo.RealName = user.RealName;
                    userInfo.StudentId = user.StudentId;
                    userInfo.Email = user.Email;
                    userInfo.Phone = user.Phone;
                    userInfo.Hometown = user.Hometown;
                    userInfo.Birthday = user.Birthday;
                    userInfo.University = user.University;
                    userInfo.Introduction = user.Introduction;
                    userInfo.Password = "";

                    res.Add(userInfo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = jss.Serialize(res);
            context.Response.ContentType = "json";
            context.Response.Write(json);
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addUser(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            bool suc = true;
            string emsg="";
            User u = Json.parse<User>(context.Request["parameter"]);
            //default authority(=1) is set by constructor of class "User"
            UserGroup ug = new UserGroup();
            Table<User> uTable = dc.GetTable<User>();
            Table<UserGroup> ugTable = dc.GetTable<UserGroup>();
            if (u.GroupName == null && string.IsNullOrEmpty(u.GroupName) == true)
            {
                ug.Groupname = "Default";
            }
            else
            {
                ug.Groupname = u.GroupName;
            }
            ug.Username = u.Name;
            try
            {
                uTable.InsertOnSubmit(u);
                ugTable.InsertOnSubmit(ug);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Username already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.ContentType = "json";
            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                string avatar_name;
                avatar_name = "a_";
                avatar_name += u.Name;
                try
                {
                    bool isExists = System.IO.Directory.Exists(context.Server.MapPath("~/user_data/" + u.Name));
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(context.Server.MapPath("~/user_data/" + u.Name));
                    string savePath = "~/user_data/" + u.Name + "/" + avatar_name + ".jpg";
                    System.IO.File.Copy(context.Server.MapPath("~/media/image/default-avatar.jpg"),
                        context.Server.MapPath(savePath), false);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                string resp = "{\"r\":\"s\"," + Json.stringify(u).Substring(1);
                context.Response.Write(resp);
            }                
        }

        public void editUser(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            Table<User> uTable = dc.GetTable<User>();
            Table<UserGroup> ugTable = dc.GetTable<UserGroup>();

            string tmp = context.Request["parameter"];
            string json = "";
            bool suc = true;
            string emsg = "";
            User u = Json.parse<User>(context.Request["parameter"]);
            bool passedit = true;
            if (context.Request["passedit"] == null || context.Request["passedit"] != "true")
                passedit = false;
            try
            {
                if (u.Name != null)
                {
                    var query = from user in uTable
                                where user.Name == u.Name
                                select user;
                    if (query.Count() > 0)
                    {
                        User u_old = query.FirstOrDefault();
                        if (passedit)
                            u_old.Password = u.Password;
                        //u_old.Name = u.Name; //must be same holded by js
                        u_old.RealName = u.RealName;
                        u_old.Authority = u.Authority;
                        u_old.StudentId = u.StudentId;
                        u_old.Email = u.Email;
                        u_old.Phone = u.Phone;
                        u_old.Hometown = u.Hometown;
                        u_old.Birthday = u.Birthday;
                        u_old.University = u.University;
                        u_old.Introduction = u.Introduction;

                        if (u_old.GroupName != u.GroupName)
                        {
                            // update User Group
                            var query2 = from userg in ugTable
                                        where u_old.Name == userg.Username &&
                                              u_old.GroupName == userg.Groupname
                                        select userg;
                            UserGroup ug_old = query2.FirstOrDefault();
                            if (ug_old != null)
                            {
                                ugTable.DeleteOnSubmit(ug_old);
                            }
                            UserGroup ug_new = new UserGroup();
                            ug_new.Username = u.Name;
                            ug_new.Groupname = u.GroupName;
                            ugTable.InsertOnSubmit(ug_new);
                        }
                        u_old.GroupName = u.GroupName;
                    }
                    else if (passedit)
                    {
                        dc.GetTable<User>().InsertOnSubmit(u);
                    }
                    else { }
                    dc.SubmitChanges();
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Username already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);

            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                // Daniel; Should check if update success and impl js logic, not just display it simply...
                json = Json.stringify(u);
                context.Response.Write(json);
            }
        }

        public void deleteUser(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            User u = Json.parse<User>(context.Request["parameter"]);
            // remove user data

            try
            {
                Table<User> uTable = dc.GetTable<User>();
                Table<UserGroup> ugTable = dc.GetTable<UserGroup>();

                var query = from user in uTable
                            where user.Name == u.Name
                            select user;

                User u_to_del = query.FirstOrDefault();
                if (query.FirstOrDefault() != null)
                {
                    var query2 = from userg in ugTable
                                 where u_to_del.Name == userg.Username &&
                                       u_to_del.GroupName == userg.Groupname
                                 select userg;
                    UserGroup ug_old = query2.FirstOrDefault();
                    if (ug_old != null)
                    {
                        ugTable.DeleteOnSubmit(ug_old);
                    }
                    uTable.DeleteOnSubmit(u_to_del);
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.Write(Json.stringify("success"));
        }

        // ---- End User Management ----

        // ---- Start User Group Management ----
        public void getAllGroups(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<UserGroup> res = new List<UserGroup>();

            try
            {
                var query = from user in dc.GetTable<UserGroup>()
                            select user;

                foreach (var user in query)
                {
                    UserGroup userInfo = new UserGroup();
                    userInfo.Username = user.Username;
                    userInfo.Groupname = user.Groupname;

                    res.Add(userInfo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = jss.Serialize(res);
            context.Response.ContentType = "json";
            context.Response.Write(json);
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addGroup(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            bool suc = true;
            string emsg = "";
            UserGroup u = Json.parse<UserGroup>(context.Request["parameter"]);

            try
            {
                dc.GetTable<UserGroup>().InsertOnSubmit(u);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "User Group relationship already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.ContentType = "json";
            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                string resp = "{\"r\":\"s\"," + Json.stringify(u).Substring(1);
                context.Response.Write(resp);
            }
        }

        public void editGroup(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            string tmp = context.Request["parameter"];
            string json = "";
            bool suc = true;
            string emsg = "";
            UserGroup u = Json.parse<UserGroup>(context.Request["parameter"]);
            Table<UserGroup> uTable = dc.GetTable<UserGroup>();
            try
            {
                if (u.Username != null && u.Groupname != null)
                {
                    var query = from user in uTable
                                where user.Username == u.Username &&
                                      user.Groupname == u.Groupname
                                select user;
                    if (query.Count() > 0)
                    {
                        UserGroup u_old = query.FirstOrDefault();
                        uTable.DeleteOnSubmit(u_old);
                        uTable.InsertOnSubmit(u);
                    }
                    else { }
                    dc.SubmitChanges();
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "User Group relationship already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);

            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                // Daniel; Should check if update success and impl js logic, not just display it simply...
                json = Json.stringify(u);
                context.Response.Write(json);
            }
        }

        public void deleteGroup(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            UserGroup u = Json.parse<UserGroup>(context.Request["parameter"]);

            try
            {
                Table<UserGroup> uTable = dc.GetTable<UserGroup>();

                var query = from user in uTable
                            where user.Username == u.Username &&
                                  user.Groupname == u.Groupname
                            select user;

                UserGroup u_to_del = query.FirstOrDefault();
                if (query.FirstOrDefault() != null)
                {
                    uTable.DeleteOnSubmit(u_to_del);
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.Write(Json.stringify("success"));
        }

        // ---- End User Group Management ----

        // ---- Start Profile Paper Edit ----
        class Pubres
        {
            public string text { get; set; }
            public string r { get; set; }
            public string PaperName { get; set; }
            public string Conference { get; set; }
            public int Year { get; set; }
            public int Id { get; set; }

            public Pubres()
            {
            }

            public Pubres(string r)
            {
                this.r = r;
            }

            public Pubres(Publication p, string r = "")
            {
                this.Id = p.Id;
                this.PaperName = p.PaperName;
                this.Conference = p.Conference;
                this.Year = p.Year;
                this.text = p.PaperName + ", " + p.Conference + "' " + p.Year.ToString();
                this.r = r;
            }
        };

        public void getAllPapers(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Publication> res = new List<Publication>();
            List<Pubres> res_str = new List<Pubres>();

            try
            {
                var query = from user in dc.GetTable<Publication>()
                            select user;

                foreach (var p in query)
                {
                    Publication pInfo = new Publication();
                    pInfo.Id = p.Id;
                    pInfo.UserName = p.UserName;
                    pInfo.PaperName = p.PaperName;
                    pInfo.Conference = p.Conference;
                    pInfo.Year = p.Year;
                    pInfo.Time = p.Time;
                    res.Add(pInfo);
                    Pubres pRes = new Pubres(pInfo, "s");
                    res_str.Add(pRes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = jss.Serialize(res_str);
            context.Response.ContentType = "json";
            context.Response.Write(json);
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addPaper(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool suc = true;
            string emsg = "";
            Publication p = Json.parse<Publication>(context.Request["parameter"]);
            //default authority(=1) is set by constructor of class "User"

            try
            {
                //TODO: set others paper..
                p.UserName = context.Session["_Login_Name"].ToString();
                p.Time = DateTime.Now;
                dc.GetTable<Publication>().InsertOnSubmit(p);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Duplicated Insert!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.ContentType = "json";
            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                Pubres pRes = new Pubres(p, "s");
                string resp = "{\"r\":\"s\"," + jss.Serialize(pRes).Substring(1);
                context.Response.Write(resp);
            }
        }

        public void editPaper(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string tmp = context.Request["parameter"];
            bool suc = true;
            string emsg = "";
            Publication p = Json.parse<Publication>(context.Request["parameter"]);

            var query = from pub in dc.GetTable<Publication>()
                        where pub.Id == p.Id
                        select pub;
            try{
                if (query.Count() > 0)
                {
                    Publication p_old = query.FirstOrDefault();
                    p_old.PaperName = p.PaperName;
                    p_old.Conference = p.Conference;
                    p_old.Year = p.Year;
                }
                else {
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Paper already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);

            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                // Daniel; Should check if update success and impl js logic, not just display it simply...
                Pubres pRes = new Pubres(p, "s");
                string resp = "{\"r\":\"s\"," + jss.Serialize(pRes).Substring(1);
                context.Response.Write(resp);
            }
        }

        public void deletePaper(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            Publication p = Json.parse<Publication>(context.Request["parameter"]);
            // remove user data

            try
            {
                Table<Publication> pTable = dc.GetTable<Publication>();

                var query = from pub in pTable
                            where pub.Id == p.Id
                            select pub;

                Publication p_to_del = query.FirstOrDefault();
                if (query.FirstOrDefault() != null)
                {
                    pTable.DeleteOnSubmit(p_to_del);
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.Write(Json.stringify("success"));
        }

        // ---- End Profile Paper Edit ----

        // ---- Start Profile Award Edit ----
        class Awdres
        {
            public string text { get; set; }
            public string r { get; set; }
            public string Name { get; set; }
            public int Year { get; set; }
            public int Id { get; set; }

            public Awdres()
            {
            }

            public Awdres(string r)
            {
                this.r = r;
            }

            public Awdres(Award p, string r = "")
            {
                this.Id = p.Id;
                this.Name = p.Name;
                this.Year = p.Year;
                this.text = p.Name + ", " + p.Year.ToString();
                this.r = r;
            }
        };

        public void getAllAwards(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Award> res = new List<Award>();
            List<Awdres> res_str = new List<Awdres>();

            try
            {
                var query = from awd in dc.GetTable<Award>()
                            select awd;

                foreach (var p in query)
                {
                    Award pInfo = new Award();
                    pInfo.Id = p.Id;
                    pInfo.UserName = p.UserName;
                    pInfo.Name = p.Name;
                    pInfo.Year = p.Year;
                    pInfo.Time = p.Time;
                    res.Add(pInfo);
                    Awdres pRes = new Awdres(pInfo, "s");
                    res_str.Add(pRes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = jss.Serialize(res_str);
            context.Response.ContentType = "json";
            context.Response.Write(json);
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addAward(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool suc = true;
            string emsg = "";
            Award p = Json.parse<Award>(context.Request["parameter"]);
            //default authority(=1) is set by constructor of class "User"

            try
            {
                //TODO: set others awards..
                p.UserName = context.Session["_Login_Name"].ToString();
                p.Time = DateTime.Now;
                dc.GetTable<Award>().InsertOnSubmit(p);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Duplicated Insert!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.ContentType = "json";
            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                Awdres pRes = new Awdres(p, "s");
                string resp = "{\"r\":\"s\"," + jss.Serialize(pRes).Substring(1);
                context.Response.Write(resp);
            }
        }

        public void editAward(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string tmp = context.Request["parameter"];
            bool suc = true;
            string emsg = "";
            Award p = Json.parse<Award>(context.Request["parameter"]);

            var query = from pub in dc.GetTable<Award>()
                        where pub.Id == p.Id
                        select pub;
            try
            {
                if (query.Count() > 0)
                {
                    Award p_old = query.FirstOrDefault();
                    p_old.Name = p.Name;
                    p_old.Year = p.Year;
                }
                else
                {
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                suc = false;
                emsg = e.ToString();
                if (emsg.Contains("DuplicateKeyException"))
                {
                    emsg = "Award already exists!";
                }
                else
                {
                    emsg = "Your request is rejected by server..";
                }
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);

            if (!suc)
            {
                context.Response.StatusCode = 500;
                string errback = Json.stringify(emsg);
                context.Response.Write(emsg);
            }
            else
            {
                // Daniel; Should check if update success and impl js logic, not just display it simply...
                Awdres pRes = new Awdres(p, "s");
                string resp = "{\"r\":\"s\"," + jss.Serialize(pRes).Substring(1);
                context.Response.Write(resp);
            }
        }

        public void deleteAward(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            Award p = Json.parse<Award>(context.Request["parameter"]);
            // remove Award data

            try
            {
                Table<Award> pTable = dc.GetTable<Award>();

                var query = from pub in pTable
                            where pub.Id == p.Id
                            select pub;

                Award p_to_del = query.FirstOrDefault();
                if (query.FirstOrDefault() != null)
                {
                    pTable.DeleteOnSubmit(p_to_del);
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            context.Response.Write(Json.stringify("success"));
        }

        // ---- End Profile Award Edit ----

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
