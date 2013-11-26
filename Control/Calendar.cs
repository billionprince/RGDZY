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
        [Column(Name = "Username", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Username { get; set; }

        [Column(Name = "type", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Type { get; set; }

        [Column(Name = "Start", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Start { get; set; }

        [Column(Name = "End", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
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