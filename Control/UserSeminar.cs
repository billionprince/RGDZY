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
    [Table(Name = "UserSeminar")]
    public class UserSeminar
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "UserName", UpdateCheck = UpdateCheck.Never)]
        public string UserName { get; set; }

        [Column(Name = "SeminarId", UpdateCheck = UpdateCheck.Never)]
        public int SeminarId { get; set; }

        public UserSeminar()
        {
        }
    }
}