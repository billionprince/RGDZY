using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name="File")]
    public class FileStatus
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id {get; set;}

        [Column(Name = "Name", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Name {get; set;}

        [Column(Name = "OriginName", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string OriginName {get; set;}

        [Column(Name = "Type", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string Type {get; set;}

        [Column(Name = "Size", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public int Size {get; set;}

        [Column(Name = "ThumbnailUrl", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string ThumbnailUrl {get; set;}

        [Column(Name = "FilePath", IsPrimaryKey = false, UpdateCheck = UpdateCheck.Never)]
        public string FilePath { get; set; }

        FileStatus(int id, string name, string originName, string type, int size, string thumbnailUrl)
        {
            Id = id;
            OriginName = originName;
            Type = type;
            Size = size;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}