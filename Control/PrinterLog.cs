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
        [Column(IsPrimaryKey = true, Name = "PrintTime", UpdateCheck = UpdateCheck.Never)]
        public string PrintTime { get; set; }

        [Column(Name = "UserName", UpdateCheck = UpdateCheck.Never)]
        public string Creator { get; set; }

        [Column(Name = "FileName", UpdateCheck = UpdateCheck.Never)]
        public string Content { get; set; }

        public PrinterLog()
        {
        }
    }
}