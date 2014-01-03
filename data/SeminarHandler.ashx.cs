using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RGDZY.control;
using System.Data.Linq;

namespace RGDZY.data
{
    /// <summary>
    /// SeminarHandler 的摘要说明
    /// </summary>
    public class SeminarHandler : IHttpHandler
    {
        private static Dictionary<string, string> week = new Dictionary<string, string> { { "MON", "0" }, { "TUE", "1" }, { "WED", "2" }, { "THUR", "3" }, { "FRI", "4" }, { "SAT", "5" }, { "SUN", "6" } };

        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["command"];
            if (command != null)
            {
                System.Reflection.MethodInfo method = this.GetType().GetMethod(command);
                if (method != null)
                {
                    method.Invoke(this, new object[] { context });
                    return;
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Error");
        }

        public void getAllSeminars(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            List<Seminar> semList = new List<Seminar>();

            try
            {
                var query = from seminar in dc.GetTable<Seminar>()
                            select seminar;
                foreach (var seminar in query)
                {
                    semList.Add(seminar);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = Json.stringify(semList);
            context.Response.ContentType = "json";
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addSeminar(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            Seminar seminar = Json.parse<Seminar>(context.Request["parameter"]);

            try
            {

                dc.GetTable<Seminar>().InsertOnSubmit(seminar);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.ContentType = "json";
            context.Response.Write(Json.stringify(seminar));

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void editSeminar(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            Seminar seminar = Json.parse<Seminar>(context.Request["parameter"]);

            try
            {

                var query = from sem in dc.GetTable<Seminar>()
                            where sem.Id == seminar.Id
                            select sem;
                //edit
                if (query.Count() > 0)
                {
                    var sem = query.First();

                    //del the calendar item first
                    if (sem.CalendarId != null)
                    {
                        Table<Calendar> tableCal = dc.GetTable<Calendar>();
                        var query2 = from c in tableCal
                                     where c.Id == sem.CalendarId
                                     select c;
                        if (query2.Count() > 0) 
                        {
                            tableCal.DeleteOnSubmit(query2.First());
                        }
                    }

                    //add a new calendar item
                    Calendar cal = new Calendar();
                    cal.Id = Guid.NewGuid();
                    cal.Type = 2;
                    cal.Title = seminar.Name;
                    cal.Detail = seminar.Name;
                    cal.Allday = 0;
                    cal.Start = week[seminar.Day] + " " + seminar.BeginTime + ":00";
                    cal.End = week[seminar.Day] + " " + seminar.EndTime + ":00";
                    cal.Creator = context.Request["creator"];
                    cal.Url = null;
                    cal.Participant = seminar.Participator;

                    sem.CalendarId = cal.Id;
                    dc.GetTable<Calendar>().InsertOnSubmit(cal);
                    
                    sem.Name = seminar.Name;
                    sem.Participator = seminar.Participator;
                    sem.Day = seminar.Day;
                    sem.BeginTime = seminar.BeginTime;
                    sem.EndTime = seminar.EndTime;

                    dc.SubmitChanges();
                }
                else//add
                {
                    Calendar cal = new Calendar();
                    cal.Id = Guid.NewGuid();
                    cal.Type = 2;
                    cal.Title = seminar.Name;
                    cal.Detail = seminar.Name;
                    cal.Allday = 0;
                    cal.Start = week[seminar.Day] + " " + seminar.BeginTime + ":00";
                    cal.End = week[seminar.Day] + " " + seminar.EndTime + ":00";
                    cal.Creator = context.Request["creator"];
                    cal.Url = null;
                    cal.Participant = seminar.Participator;

                    seminar.CalendarId = cal.Id;

                    dc.GetTable<Calendar>().InsertOnSubmit(cal);
                    dc.GetTable<Seminar>().InsertOnSubmit(seminar);
                    dc.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var json = Json.stringify(seminar);
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void deleteSeminar(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            Seminar seminar = Json.parse<Seminar>(context.Request["parameter"]);

            try
            {
                var query = from sem in dc.GetTable<Seminar>()
                            where sem.Id == seminar.Id
                            select sem;
                foreach (var sem in query)
                {
                    if (sem.CalendarId != null)
                    {
                        Table<Calendar> tableCal = dc.GetTable<Calendar>();
                        var query2 = from cal in tableCal
                                     where cal.Id == sem.CalendarId
                                     select cal;
                        if (query2.Count() > 0)
                        {
                            tableCal.DeleteOnSubmit(query2.First());
                        }
                    }
                    dc.GetTable<Seminar>().DeleteOnSubmit(sem);
                }

                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.Write(Json.stringify("success"));
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}