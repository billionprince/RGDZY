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
    [Table(Name = "Milestone")]
    public class Milestone
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "ProjectId", UpdateCheck = UpdateCheck.Never)]
        public int ProjectId { get; set; }

        [Column(Name = "Description", UpdateCheck = UpdateCheck.Never)]
        public string Description { get; set; }

        [Column(Name = "Time", UpdateCheck = UpdateCheck.Never)]
        public DateTime Time { get; set; }

        [Column(Name = "ImagePath", UpdateCheck = UpdateCheck.Never)]
        public string ImagePath { get; set; }
        
        public Milestone()
        {
        }
    }

}
