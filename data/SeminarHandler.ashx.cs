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

        public void getUserSeminars(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            List<Seminar> semList = new List<Seminar>();

            try
            {
                string username = context.Request["user"];
                var sTable = dc.GetTable<Seminar>();
                var usTable = dc.GetTable<UserSeminar>();
                var query = from us in usTable
                            where us.UserName == username
                            select us;
                foreach (var us in query)
                {
                    var sem = sTable.First(s => s.Id == us.SeminarId);
                    if (sem != null)
                    {
                        semList.Add(sem);
                    }
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

                    //edit user seminar
                    string par = sem.Participator;
                    string[] pars = par.Split(',');

                    var usTable = dc.GetTable<UserSeminar>();
                    var userDic = new Dictionary<string, UserSeminar>();
                    var query3 = from us in usTable
                                 where us.SeminarId == sem.Id
                                 select us;
                    foreach (var us in query3)
                    {
                        userDic.Add(us.UserName, us);
                    }

                    //add new user semianr
                    foreach (string s in pars)
                    {
                        if (userDic.ContainsKey(s))
                        {
                            userDic.Remove(s);
                        }
                        else
                        {
                            var us = new UserSeminar();
                            us.SeminarId = sem.Id;
                            us.UserName = s;
                            usTable.InsertOnSubmit(us);
                        }
                    }

                    //del old user seminar
                    usTable.DeleteAllOnSubmit(userDic.Values);

                    dc.SubmitChanges();

                }
                else//add
                {
                    //add calendar and seminar
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

                    //add user seminar
                    string par = seminar.Participator;
                    string[] pars = par.Split(',');

                    var usTable = dc.GetTable<UserSeminar>();
                    foreach (string s in pars)
                    {
                        var us = new UserSeminar();
                        us.SeminarId = seminar.Id;
                        us.UserName = s;
                        usTable.InsertOnSubmit(us);
                    }
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

                //delete user seminar
                var usTable = dc.GetTable<UserSeminar>();
                var query3 = from us in usTable
                            where us.SeminarId == seminar.Id
                            select us;
                usTable.DeleteAllOnSubmit(query3);

                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.Write(Json.stringify("success"));
            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        //get seminar by id
        public void getSeminar(HttpContext context)
        {
            List<Dictionary<string, object>> rec = new List<Dictionary<string, object>>();
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            try
            {
                int sid = Int32.Parse(context.Request["id"]);
                var sTable = dc.GetTable<Seminar>();
                var query = from s in sTable
                            where s.Id == sid
                            select s;
                string json = "";
                if (query.Count() > 0)
                {
                    json = Json.stringify(query.First());
                }

                context.Response.ContentType = "json";
                context.Response.Write(json);
            }
            catch (System.Exception ex)
            {
                string msg = "Error occured while executing getSeminar:";
                msg += ex.Message;
                //throw new Exception(msg);
            }
            finally
            {
                DBConnectionSingleton.Instance.ReturnDBConnection(dc);
            }

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