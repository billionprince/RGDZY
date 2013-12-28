using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace RGDZY.control
{
    [Table(Name = "Calendar")]
    public class Calendar
    {
        [Column(Name = "id", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public Guid Id { get; set; }

        //type: once=0, daily=1, weekly=2, monthly=3, yearly=4, personly=5
        [Column(Name = "type", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int Type { get; set; }

        //"All members"
        //"Group A, Group B, Group C, User1, User2"
        [Column(Name = "owner", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public string Participant { get; set; }

        [Column(Name = "title", UpdateCheck = UpdateCheck.Never)]
        public string Title { get; set; }

        [Column(Name = "start_time", UpdateCheck = UpdateCheck.Never)]
        public string Start { get; set; }

        [Column(Name = "end_time", UpdateCheck = UpdateCheck.Never)]
        public string End { get; set; }

        [Column(Name = "allday", UpdateCheck = UpdateCheck.Never)]
        public int Allday { get; set; }

        [Column(Name = "creator", UpdateCheck = UpdateCheck.Never)]
        public string Creator { get; set; }

        [Column(Name = "url", UpdateCheck = UpdateCheck.Never)]
        public string Url { get; set; }

        [Column(Name = "detail", UpdateCheck = UpdateCheck.Never)]
        public string Detail { get; set; }

        [Column(Name = "sendemail", UpdateCheck = UpdateCheck.Never)]
        public int Sendemail { get; set; }

        public Calendar()
        {
        }

        public Calendar(int type, string owner, int allday, string title, string creator, string detail=null, 
            string start_time=null, string end_time=null, string url=null, int sendemail=0)
        {
            Id = Guid.NewGuid();
            Type = type;
            Participant = owner;
            Title = title;
            Start = start_time;
            End = end_time;
            Allday = allday;
            Creator = creator;
            Url = url;
            Detail = detail;
            Sendemail = sendemail;
        }
    }
}