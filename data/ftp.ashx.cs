﻿using System;
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
    /// Summary description for get_user_calendar
    /// </summary>
    public class ftp : IHttpHandler
    {
        private enum Response { success, fail };

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

        public void edit_ftp_account(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                string username = context.Request["username"];
                var query = from u in dc.GetTable<User>()
                            where u.Name == username
                            select u;
                var myinfo = query.First();
                myinfo.FTPUsername = context.Request["ftpusername"];
                myinfo.FTPPassword = context.Request["ftppassword"];
                dc.SubmitChanges();

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(myinfo));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing edit_ftp_account:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
        }

        public void get_ftp_settings(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                var query = from u in dc.GetTable<User>()
                            select new
                            {
                                u.Name,
                                u.FTPUsername,
                                u.FTPPassword
                            };
                foreach (var obj in query)
                {
                    Dictionary<string, object> evt = new Dictionary<string, object>();
                    evt.Add("username", obj.Name);
                    evt.Add("ftpusername", obj.FTPUsername);
                    evt.Add("ftppassword", obj.FTPPassword);
                    rec.Add(evt);
                }

                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(rec));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing get_ftp_settings:";
                msg += ex.Message;
                throw new Exception(msg);
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