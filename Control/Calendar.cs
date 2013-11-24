using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "Calendar")]
    public class Calendar
    {
        [Column(IsPrimaryKey = true, Name = "Username", UpdateCheck = UpdateCheck.Never)]
        public string Username { get; set; }

        [Column(IsPrimaryKey = true, Name = "type", UpdateCheck = UpdateCheck.Never)]
        public string Type { get; set; }

        [Column(IsPrimaryKey = true, Name = "Start", UpdateCheck = UpdateCheck.Never)]
        public string Start { get; set; }

        [Column(IsPrimaryKey = true, Name = "End", UpdateCheck = UpdateCheck.Never)]
        public string End { get; set; }

        [Column(Name = "Creator", UpdateCheck = UpdateCheck.Never)]
        public string Creator { get; set; }

        [Column(Name = "Content", UpdateCheck = UpdateCheck.Never)]
        public string Content { get; set; }

        public Calendar()
        {
        }
    }
}