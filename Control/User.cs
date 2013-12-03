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
        [Column(IsPrimaryKey = true, Name = "Id", UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column(Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        [Column(Name = "StudentId", UpdateCheck = UpdateCheck.Never)]
        public string StudentId { get; set; }

        [Column(Name = "Authority", UpdateCheck = UpdateCheck.Never)]
        public int Authority { get; set; }

        [Column(Name = "Password", UpdateCheck = UpdateCheck.Never)]
        public string Password { get; set; }

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
