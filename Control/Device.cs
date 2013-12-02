using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RGDZY.control
{
/*    public class SimpleDevice
    {
        public int id { get; set; }
        public string assetNum { get; set; }

        public EntitySet<DeviceUse> DeviceUse;

        public SimpleDevice()
        {
            this.DeviceUse = new EntitySet<DeviceUse>();
        }
    }
    [Table(Name="Device")]
    public class Device
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated=true, UpdateCheck = UpdateCheck.Never)]
        public int id { get; set; }

        [Column(IsPrimaryKey = false, Name = "AssetNum", UpdateCheck = UpdateCheck.Never)]
        public string  assetNum { get; set; }

        [Column(IsPrimaryKey = false, Name = "Type", UpdateCheck = UpdateCheck.Never)]
        public string type { get; set; }

        [Column(IsPrimaryKey = false, Name = "Version", UpdateCheck = UpdateCheck.Never)]
        public string version { get; set; }

        [Column(IsPrimaryKey = false, Name = "Cpu", UpdateCheck = UpdateCheck.Never)]
        public string cpu { get; set; }

        [Column(IsPrimaryKey = false, Name = "Memory", UpdateCheck = UpdateCheck.Never)]
        public string memory { get; set; }

        [Column(IsPrimaryKey = false, Name = "Disk", UpdateCheck = UpdateCheck.Never)]
        public string disk { get; set; }

        [Column(IsPrimaryKey = false, Name = "PurchaseDate", UpdateCheck = UpdateCheck.Never)]
        public DateTime purchaseDate { get; set; }

        [Column(IsPrimaryKey = false, Name = "Remark", UpdateCheck = UpdateCheck.Never)]
        public string remark { get; set; }

        private EntitySet<DeviceUse> _DeviceUse;

        [Association(Name = "FK_DeviceUse_Device", Storage = "_DeviceUse", ThisKey = "id", OtherKey = "deviceId")]
        public EntitySet<DeviceUse> DeviceUse
        {
            get { return this._DeviceUse; }
            set { this._DeviceUse.Assign(value); }
        }

        public Device()
        {
            this._DeviceUse = new EntitySet<DeviceUse>();
        }

    }*/
}