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


    [Table(Name = "DeviceUse")]
    public partial class DeviceUse
    {
        [Column(Name = "DeviceId", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int DeviceId { get; set; }

        [Column(Name = "UserId", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string UserId { get; set; }

        [Column(Name = "StartDate", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public Nullable<DateTime> StartDate { get; set; }

        [Column(Name = "EndDate", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public Nullable<DateTime> EndDate { get; set; }
    }

}