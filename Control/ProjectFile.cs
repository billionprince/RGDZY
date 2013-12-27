using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "ProjectFile")]
    public class ProjectFile
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "ProjectId", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public int ProjectId { get; set; }

        [Column(Name = "FileId", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public int FileId { get; set; }

        public ProjectFile()
        {
        }
    }
}