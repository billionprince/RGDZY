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

        public void getUserDevices(HttpContext context)
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

        public void deleteDevice(HttpContext context)
        {
            DBConnectionSingletion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingletion.Instance.BorrowDBConnection();

            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            Table<Device> devTable = dc.GetTable<Device>();
            Table<DeviceUse> devUseTable = dc.GetTable<DeviceUse>();

            var query = from dev in devTable
                        where dev.Id == du.dev.Id
                        select dev;

            foreach (var dev in query){
                if (dev.DeviceUse != null)
                {
                    devUseTable.DeleteOnSubmit(dev.DeviceUse);
                    dc.SubmitChanges();
                }
                devTable.DeleteOnSubmit(dev);
                dc.SubmitChanges();
            }

            context.Response.Write(Json.stringify("success"));

            DBConnectionSingletion.Instance.ReturnDBConnection(dc);

        }

        public void editDevice(HttpContext context) 
        {
            DBConnectionSingletion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingletion.Instance.BorrowDBConnection();

            string tmp = context.Request["parameter"];
            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            var query = from dev in dc.GetTable<Device>()
                        where dev.Id == du.dev.Id
                        select dev;

            string json = "";

            foreach (var dev in query)
            {
                if (dev.DeviceUse != null)
                {
                    if (du.devUse == null || du.devUse.UserId.Length == 0)
                    {
                        dc.GetTable<DeviceUse>().DeleteOnSubmit(dev.DeviceUse);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        dev.DeviceUse.DeviceId = du.devUse.DeviceId;
                        dev.DeviceUse.EndDate = du.devUse.EndDate;
                        dev.DeviceUse.StartDate = du.devUse.StartDate;
                        dev.DeviceUse.UserId = du.devUse.UserId;
                        dc.SubmitChanges();
                    }
                }
                else
                {
                    if (du.devUse != null && du.devUse.UserId.Length != 0)
                    {
                        dc.GetTable<DeviceUse>().InsertOnSubmit(du.devUse);
                        dc.SubmitChanges();
                    }
                }
                dev.AssetNum = du.dev.AssetNum;
                dev.Cpu = du.dev.Cpu;
                dev.Disk = du.dev.Disk;
                dev.Memory = du.dev.Memory;
                dev.PurchaseDate = du.dev.PurchaseDate;
                dev.Remark = du.dev.Remark;
                dev.Type = du.dev.Type;
                dev.Version = du.dev.Version;
                dc.SubmitChanges();

                DeviceUse2 du2;
                if (dev.DeviceUse != null)
                {
                    du2 = new DeviceUse2() { dev = dev, devUse = dev.DeviceUse };
                }
                else
                {
                    du2 = new DeviceUse2() { dev = dev, devUse = new DeviceUse() };
                }

                json = Json.stringify(du);
            }
            
            context.Response.Write(json);

            DBConnectionSingletion.Instance.ReturnDBConnection(dc);
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