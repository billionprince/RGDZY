using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "User")]
    public class User
    {
        [Column(IsPrimaryKey = true, Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        [Column(Name = "StudentId", UpdateCheck = UpdateCheck.Never)]
        public string StudentId { get; set; }

        [Column(Name = "Authority", UpdateCheck = UpdateCheck.Never)]
        public int Authority { get; set; }

        [Column(Name = "Password", UpdateCheck = UpdateCheck.Never)]
        public string Password { get; set; }

        [Column(Name = "Introduction", UpdateCheck = UpdateCheck.Never)]
        public string Introduction { get; set; }

        [Column(Name = "Link", UpdateCheck = UpdateCheck.Never)]
        public string Link { get; set; }

        [Column(Name = "Hometown", UpdateCheck = UpdateCheck.Never)]
        public string Hometown { get; set; }

        [Column(Name = "Birthday", UpdateCheck = UpdateCheck.Never)]
        public DateTime Birthday { get; set; }

        [Column(Name = "University", UpdateCheck = UpdateCheck.Never)]
        public string University { get; set; }

        [Column(Name = "Email", UpdateCheck = UpdateCheck.Never)]
        public string Email { get; set; }

        [Column(Name = "Phone", UpdateCheck = UpdateCheck.Never)]
        public string Phone { get; set; }

        [Column(Name = "RealName", UpdateCheck = UpdateCheck.Never)]
        public string RealName { get; set; }

        public User()
        {
        }
    }

    static public class UserAuthority
    {
        enum AFlag : byte { F1 = 0, F2, F3, F4, F5 };
        static bool hasAFlag(uint authority, AFlag flag)
        {
            int offset = (int)flag;
            uint mask = (uint)(0x1 << offset);
            if (((uint)authority & mask) != 0x0)
                return true;
            else
                return false;
        }
    }
}
