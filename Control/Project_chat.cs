using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "Project_chat")]
    public class Project_chat
    {
        [Column(Name = "project_id", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int project_id { get; set; }

        [Column(Name = "Id", UpdateCheck = UpdateCheck.Never)]
        public Guid Id { get; set; }

        [Column(Name = "owner", UpdateCheck = UpdateCheck.Never)]
        public string owner { get; set; }

        [Column(Name = "chat_content", UpdateCheck = UpdateCheck.Never)]
        public string chat_content { get; set; }

        [Column(Name = "chat_time", UpdateCheck = UpdateCheck.Never)]
        public string chat_time { get; set; }

        public Project_chat()
        {
        }
    }
}