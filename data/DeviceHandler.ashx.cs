using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RGDZY.control;
using System.Data.Linq;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace RGDZY.data
{
    /// <summary>
    /// DeviceHandler 的摘要说明
    /// </summary>
    /// 
    public class DeviceUse2
    {
        public Device dev;
        public DeviceUse devUse;
    }

    public class DeviceHandler : IHttpHandler
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

        public void addDevice(HttpContext context)
        {
            DBConnectionSingletion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingletion.Instance.BorrowDBConnection();

            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            dc.GetTable<Device>().InsertOnSubmit(du.dev);
            dc.SubmitChanges();

            if (du.devUse.UserId.Length != 0)
            {
                int tmp = du.dev.Id;
                du.devUse.DeviceId = du.dev.Id;
                dc.GetTable<DeviceUse>().InsertOnSubmit(du.devUse);
            }
            dc.SubmitChanges();

            context.Response.Write(Json.stringify(du));

            DBConnectionSingletion.Instance.ReturnDBConnection(dc);

        }


        public void getAllDevices(HttpContext context)
        {
            DBConnectionSingletion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingletion.Instance.BorrowDBConnection();
            
            var query = from dev in dc.GetTable<Device>()
                        select dev;

            List<DeviceUse2> devList = new List<DeviceUse2>();
            foreach (var dev in query)
            {
                if (dev.DeviceUse != null)
                {
                    devList.Add(new DeviceUse2() { dev = dev, devUse = dev.DeviceUse });
                }
                else
                {
                    devList.Add(new DeviceUse2() { dev = dev, devUse = new DeviceUse() });
                }
            }

            string json = Json.stringify(devList);
            context.Response.Write(json);

            DBConnectionSingletion.Instance.ReturnDBConnection(dc);

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