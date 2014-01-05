using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "Publication")]
    public class Publication
    {
        [Column(IsPrimaryKey = true, Name = "Id", UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "UserName", UpdateCheck = UpdateCheck.Never)]
        public string UserName { get; set; }

        [Column(Name = "PaperName", UpdateCheck = UpdateCheck.Never)]
        public string PaperName { get; set; }

        [Column(Name = "Conference", UpdateCheck = UpdateCheck.Never)]
        public string Conference { get; set; }

        [Column(Name = "Year", UpdateCheck = UpdateCheck.Never)]
        public int Year { get; set; }

        [Column(Name = "Time", UpdateCheck = UpdateCheck.Never)]
        public DateTime Time { get; set; }

        public Publication()
        {
        }
    }
}