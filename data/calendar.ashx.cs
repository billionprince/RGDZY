﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RGDZY.data
{
    /// <summary>
    /// Summary description for get_user_calendar
    /// </summary>
    public class calendar : IHttpHandler
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

        public void get_user_calendar(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, string>> rec = new List<Dictionary<string,string>>();
            context.Response.ContentType = "json";
            context.Response.Write(jss.Serialize(rec));
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