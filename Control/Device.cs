using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System;

namespace RGDZY.control
{

    [Table(Name = "Device")]
    public class Device
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "AssetNum", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string AssetNum { get; set; }

        [Column(Name = "Type", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Type { get; set; }

        [Column(Name = "Version", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Version { get; set; }

        [Column(Name = "MAC", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string MAC { get; set; }

        [Column(Name = "Cpu", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Cpu { get; set; }

        [Column(Name = "Memory", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Memory { get; set; }

        [Column(Name = "Disk", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Disk { get; set; }

        [Column(Name = "PurchaseDate", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public Nullable<DateTime> PurchaseDate { get; set; }

        [Column(Name = "Remark", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Remark { get; set; }

    }


}