using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "PrintFile")]
    public class PrintFile
    {
        [Column(Name = "id", IsPrimaryKey = true, AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "username", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string username { get; set; }

        [Column(Name = "time", UpdateCheck = UpdateCheck.Never)]
        public string time { get; set; }

        [Column(Name = "filename", UpdateCheck = UpdateCheck.Never)]
        public string filename { get; set; }

        [Column(Name = "single", UpdateCheck = UpdateCheck.Never)]
        public int single { get; set; }

        public PrintFile()
        {
        }
    }
}