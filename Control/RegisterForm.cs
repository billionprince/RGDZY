using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Web;

namespace RGDZY.control
{
    [Table(Name = "RegisterForm")]
    public class RegisterForm
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "Authority", UpdateCheck = UpdateCheck.Never)]
        public int Authority { get; set; }

        [Column(Name = "Email", UpdateCheck = UpdateCheck.Never)]
        public string Email { get; set; }

        [Column(Name = "Hashcode1", UpdateCheck = UpdateCheck.Never)]
        public string Hashcode1 { get; set; }

        [Column(Name = "Hashcode2", UpdateCheck = UpdateCheck.Never)]
        public string Hashcode2 { get; set; }

        [Column(Name = "Datetime", UpdateCheck = UpdateCheck.Never)]
        public DateTime Datetime { get; set; }

        // FK from User.Name
        [Column(Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        public RegisterForm()
        {
        }
    }
}