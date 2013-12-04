using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "PrinterLog")]
    public class PrinterLog
    {
	    [Column(Name = "Id", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Id { get; set; }
		
        [Column(Name = "PrintTime", UpdateCheck = UpdateCheck.Never)]
        public string PrintTime { get; set; }

        [Column(Name = "UserName", UpdateCheck = UpdateCheck.Never)]
        public string UserName { get; set; }

        [Column(Name = "FileName", UpdateCheck = UpdateCheck.Never)]
        public string FileName { get; set; }

        public PrinterLog()
        {
        }
    }
}