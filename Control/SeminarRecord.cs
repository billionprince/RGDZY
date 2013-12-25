using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "SeminarRecord")]
    public class SeminarRecord
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "SeminarId", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public int SeminarId { get; set; }

        [Column(Name = "Date", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Date { get; set; }

        [Column(Name = "Recorder", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Recorder { get; set; }

        [Column(Name = "Agenda", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Agenda { get; set; }

        [Column(Name = "Appendix", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Appendix { get; set; }

    }
}