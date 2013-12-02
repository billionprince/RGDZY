using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "UserGroup")]
    public class UserGroup
    {        
        [Column(Name = "groupname", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Groupname { get; set; }

        [Column(Name = "username", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Username { get; set; }

        public UserGroup()
        {
        }
    }
}