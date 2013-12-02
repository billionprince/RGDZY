using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RGDZY.control
{
 /*   [Table(Name = "DeviceUse")]
    public class DeviceUse
    {
        [Column(IsPrimaryKey = true, Name = "DeviceId", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int deviceId { get; set; }

        [Column(IsPrimaryKey = false, Name = "UserId", UpdateCheck = UpdateCheck.Never)]
        public string UserId { get; set; }

        [Column(IsPrimaryKey = false, Name = "StartDate", UpdateCheck = UpdateCheck.Never)]
        public string startTime { get; set; }

        [Column(IsPrimaryKey = false, Name = "EndDate", UpdateCheck = UpdateCheck.Never)]
        public string endTime { get; set; }

        private EntityRef<Device> _Device;

        [Association(Name = "FK_DeviceUse_Device", Storage = "_Device", ThisKey = "deviceId", IsForeignKey = true)]
        public Device Device
        {
            get { return this._Device.Entity; }
            set { this._Device.Entity=value; }
        }

        public DeviceUse() 
        {
            this._Device = new EntityRef<Device>();
        }
    }
  * */
}