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
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            string name = context.Request["name"];
            List<DeviceUse2> devList = new List<DeviceUse2>();

            try
            {
                var query = from devUse in dc.GetTable<DeviceUse>()
                            where devUse.UserId == name
                            select devUse;


                foreach (var devUse in query)
                {
                    Device device = (from dev in dc.GetTable<Device>()
                                     where dev.Id == devUse.DeviceId
                                     select dev).First();
                    if (device != null)
                    {
                        devList.Add(new DeviceUse2() { dev = device, devUse = devUse });
                    }
                    else
                    {
                        devList.Add(new DeviceUse2() { dev = new Device(), devUse = devUse });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.ContentType = "json";
            string json = Json.stringify(devList);
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void deleteDevice(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            try
            {
                Table<Device> devTable = dc.GetTable<Device>();
                Table<DeviceUse> devUseTable = dc.GetTable<DeviceUse>();

                var query = from devUse in devUseTable
                            where devUse.DeviceId == du.dev.Id
                            select devUse;

                foreach (var devUse in query)
                {
                    devUseTable.DeleteOnSubmit(devUse);
                }

                var query1 = from dev in devTable
                             where dev.Id == du.dev.Id
                             select dev;

                foreach (var dev in query1)
                {
                    devTable.DeleteOnSubmit(dev);
                }

                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.Write(Json.stringify("success"));
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void editDevice(HttpContext context) 
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            string tmp = context.Request["parameter"];
            string json = "";

            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            try
            {
                if (du.devUse != null && du.devUse.UserId.Length != 0)
                {
                    var query = from devUse in dc.GetTable<DeviceUse>()
                                where devUse.DeviceId == du.dev.Id
                                select devUse;
                    if (query.Count() > 0)
                    {
                        foreach (var devUse in query)
                        {
                            devUse.DeviceId = du.devUse.DeviceId;
                            devUse.EndDate = du.devUse.EndDate;
                            devUse.StartDate = du.devUse.StartDate;
                            devUse.UserId = du.devUse.UserId;
                            dc.SubmitChanges();
                        }
                    }
                    else
                    {
                        dc.GetTable<DeviceUse>().InsertOnSubmit(du.devUse);
                    }
                }
                else
                {
                    var query = from devUse in dc.GetTable<DeviceUse>()
                                where devUse.DeviceId == du.dev.Id
                                select devUse;
                    if (query.Count() > 0)
                    {
                        foreach (var devUse in query)
                        {
                            dc.GetTable<DeviceUse>().DeleteOnSubmit(devUse);
                        }
                    }
                }

                var query2 = from dev in dc.GetTable<Device>()
                             where dev.Id == du.dev.Id
                             select dev;

                foreach (var dev in query2)
                {
                    dev.AssetNum = du.dev.AssetNum;
                    dev.Cpu = du.dev.Cpu;
                    dev.Disk = du.dev.Disk;
                    dev.Memory = du.dev.Memory;
                    dev.PurchaseDate = du.dev.PurchaseDate;
                    dev.Remark = du.dev.Remark;
                    dev.Type = du.dev.Type;
                    dev.Version = du.dev.Version;
                    dev.MAC = du.dev.MAC;
                }

                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            json = Json.stringify(du);
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addDevice(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            DeviceUse2 du = Json.parse<DeviceUse2>(context.Request["parameter"]);

            try
            {
                dc.GetTable<Device>().InsertOnSubmit(du.dev);
                dc.SubmitChanges();

                if (du.devUse.UserId.Length != 0)
                {
                    int tmp = du.dev.Id;
                    du.devUse.DeviceId = du.dev.Id;
                    dc.GetTable<DeviceUse>().InsertOnSubmit(du.devUse);
                }
                else
                {
                    du.devUse.EndDate = null;
                    du.devUse.StartDate = null;
                }
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.ContentType = "json";
            context.Response.Write(Json.stringify(du));

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);

        }

        public void getAllDevices(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            List<DeviceUse2> devList = new List<DeviceUse2>();

            try
            {
                var query = from dev in dc.GetTable<Device>()
                            select dev;

                foreach (var dev in query)
                {
                    DeviceUse deviceUse = (from devUse in dc.GetTable<DeviceUse>()
                                           where devUse.DeviceId == dev.Id
                                           select devUse).First();
                    if (deviceUse != null)
                    {
                        devList.Add(new DeviceUse2() { dev = dev, devUse = deviceUse });
                    }
                    else
                    {
                        devList.Add(new DeviceUse2() { dev = dev, devUse = new DeviceUse() });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = Json.stringify(devList);
            context.Response.ContentType = "json";
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void getAllUsers(HttpContext context)
        {
            DBConnectionSingleton.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();

            try
            {
                var query = from user in dc.GetTable<User>()
                            select user;

                List<string> userName = new List<String>();
                foreach (var user in query)
                {
                    Dictionary<string, object> userInfo = new Dictionary<string, object>();
                    userInfo.Add("Name", user.Name);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}