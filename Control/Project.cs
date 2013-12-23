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
    [Table(Name = "Project")]
    public class Project
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        [Column(Name = "Advisor", UpdateCheck = UpdateCheck.Never)]
        public string Advisor { get; set; }

        [Column(Name = "Description", UpdateCheck = UpdateCheck.Never)]
        public string Description { get; set; }

        [Column(Name = "FullName", UpdateCheck = UpdateCheck.Never)]
        public string FullName { get; set; }

        [Column(Name = "Link", UpdateCheck = UpdateCheck.Never)]
        public string Link { get; set; }

        public Project()
        {
        }
    }

}
