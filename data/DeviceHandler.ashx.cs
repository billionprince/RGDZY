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
using System.Net;
using System.Net.Sockets;

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
                    var query2 = from dev in dc.GetTable<Device>()
                                     where dev.Id == devUse.DeviceId
                                     select dev;
                    if (query2.Count() > 0)
                    {
                        devList.Add(new DeviceUse2() { dev = query2.First(), devUse = devUse });
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

        bool WakeUp(string macstring)
        {
            if (string.IsNullOrEmpty(macstring))
                return false;
            else if (macstring.Length != 12)
                return false;
            byte[] mac = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                mac[i] = (byte)Convert.ToInt32(macstring.Substring(i * 2, 2), 16);
            }
            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 9090);
            byte[] packet = new byte[17 * 6];
            for (int i = 0; i < 6; i++)
                packet[i] = 0xFF;
            for (int i = 1; i <= 16; i++)
                for (int j = 0; j < 6; j++)
                    packet[i * 6 + j] = mac[j];
            int result = client.Send(packet, packet.Length);
            return true;
        }

        public void PowerOnDevice(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            try
            {
                int id = int.Parse(context.Request["id"]);
                var table = dc.GetTable<Device>();
                var x = table.First(c => c.Id == id);
                string mac = x.MAC;
                bool val = WakeUp(mac);
                if (!val)
                    throw new Exception("wrong mac address!\n");
                context.Response.ContentType = "json";
                context.Response.Write(jss.Serialize(id));
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing PowerOnDevice:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }
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
                    var query2 = from devUse in dc.GetTable<DeviceUse>()
                                           where devUse.DeviceId == dev.Id
                                           select devUse;
                    if (query2.Count() > 0)
                    {
                        devList.Add(new DeviceUse2() { dev = dev, devUse = query2.First() });
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