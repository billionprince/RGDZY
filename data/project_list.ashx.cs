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
                table.InsertOnSubmit(myinfo);
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing add_project_settings:";
                msg += ex.Message;
                throw new Exception(msg);
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
                var myinfo = query.First();
                myinfo.Name = context.Request["briefname"];
                myinfo.FullName = context.Request["fullname"];
                myinfo.Description = context.Request["description"];
                myinfo.Link = context.Request["hyperlink"];
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_project_settings:";
                msg += ex.Message;
                throw new Exception(msg);
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

                context.Response.ContentType = "json";
                context.Response.Write(null);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_project_settings:";
                msg += ex.Message;
                throw new Exception(msg);
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
                    rec.Add(evt);
                }

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(rec));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_project_settings:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }
    }
}