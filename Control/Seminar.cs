using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name="Seminar")]
    public class Seminar
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id {get; set;}

        [Column(Name = "Name", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Name {get; set;}

        [Column(Name = "Day", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Day {get; set;}

        [Column(Name = "BeginTime", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string BeginTime {get; set;}

        [Column(Name = "EndTime", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string EndTime {get; set;}

        [Column(Name = "Participator", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Participator {get; set;}
    }
}