using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RGDZY.control;
using System.Data.Linq;
using System.Data.SqlClient;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for project
    /// </summary>
    public class project_list : IHttpHandler
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

        public void add_project_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                var table = dc.GetTable<Project>();
                Project myinfo = new Project();
                myinfo.Id = 0;
                myinfo.Name = context.Request["briefname"];
                myinfo.FullName = context.Request["fullname"];
                myinfo.Description = context.Request["description"];
                myinfo.Link = context.Request["hyperlink"];
                myinfo.Participator = context.Request["participator"];
                table.InsertOnSubmit(myinfo);
                dc.SubmitChanges();

                string par = context.Request["participator"];
                string[] pars = user.get_user_name(par.Split(','));

                var upTable = dc.GetTable<UserProject>();
                foreach (string s in pars)
                {
                    var up = new UserProject();
                    up.ProjectId = myinfo.Id;
                    up.UserName = s;
                    upTable.InsertOnSubmit(up);
                    
                }
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing add_project_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void edit_project_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string idstr = context.Request["id"].ToString();
                int id = int.Parse(idstr);
                var query = from u in dc.GetTable<Project>()
                            where u.Id == id
                            select u;

                //edit
                if (query.Count() > 0)
                {
                    var myinfo = query.First();
                    myinfo.Name = context.Request["briefname"];
                    myinfo.FullName = context.Request["fullname"];
                    myinfo.Description = context.Request["description"];
                    myinfo.Link = context.Request["hyperlink"];
                    myinfo.Participator = context.Request["participator"];
                    dc.SubmitChanges();

                    string par = context.Request["participator"];
                    string[] pars = user.get_user_name(par.Split(','));

                    var upTable = dc.GetTable<UserProject>();
                    var userDic = new Dictionary<string, UserProject>();
                    var query2 = from up in upTable
                                where up.ProjectId == id
                                select up;
                    foreach (var up in query2)
                    {
                        userDic.Add(up.UserName, up);
                    }

                    //add new user project
                    foreach (string s in pars)
                    {
                        if (userDic.ContainsKey(s))
                        {
                            userDic.Remove(s);
                        }
                        else 
                        {
                            var up = new UserProject();
                            up.ProjectId = myinfo.Id;
                            up.UserName = s;
                            upTable.InsertOnSubmit(up);
                        }
                    }

                    //del old user project
                    upTable.DeleteAllOnSubmit(userDic.Values);
   
                    dc.SubmitChanges();
                    context.Response.ContentType = "json";
                    context.Response.Write(jss.Serialize(myinfo));
                }
                else //add
                {
                    add_project_settings(context);
                }
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_project_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void delete_project_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int id = int.Parse(context.Request["id"]);
                var table = dc.GetTable<Project>();
                var x = table.First(c => c.Id == id);
                table.DeleteOnSubmit(x);
                dc.SubmitChanges();
                
                //delete user project
                var upTable = dc.GetTable<UserProject>();
                var query = from up in upTable
                            where up.ProjectId == id
                            select up;
                upTable.DeleteAllOnSubmit(query);

                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(null);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_project_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void get_project_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                var query = from u in dc.GetTable<Project>()
                            select u;
                foreach (var obj in query)
                {
                    Dictionary<string, object> evt = new Dictionary<string, object>();
                    evt.Add("Id", obj.Id.ToString());
                    evt.Add("BriefName", obj.Name);
                    evt.Add("FullName", obj.FullName);
                    evt.Add("Description", obj.Description);
                    evt.Add("Hyperlink", obj.Link);
                    evt.Add("Participator", obj.Participator);
                    rec.Add(evt);
                }

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(rec));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_project_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void get_user_project(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string username = context.Request["user"];
                var pTable = dc.GetTable<Project>();
                var upTable = dc.GetTable<UserProject>();
                var query = from up in upTable
                            where up.UserName == username 
                            select up;
                foreach (var up in query)
                {
                    var obj = pTable.First(p=>p.Id == up.ProjectId);
                    if (obj != null)
                    {
                        Dictionary<string, object> evt = new Dictionary<string, object>();
                        evt.Add("Id", obj.Id.ToString());
                        evt.Add("BriefName", obj.Name);
                        evt.Add("FullName", obj.FullName);
                        evt.Add("Description", obj.Description);
                        evt.Add("Hyperlink", obj.Link);
                        evt.Add("Participator", obj.Participator);
                        rec.Add(evt);
                    }
                }

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(rec));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_project_settings:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }
  
        //get project by id
        public void get_project(HttpContext context) 
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int pid = Int32.Parse(context.Request["id"]);
                var pTable = dc.GetTable<Project>();
                var query = from p in pTable
                            where p.Id == pid
                            select p;
                string json = "";
                if (query.Count() > 0)
                {
                    json = Json.stringify(query.First());
                }

                context.Response.ContentType = "json";
                context.Response.Write(json);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_project:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }
    }
}