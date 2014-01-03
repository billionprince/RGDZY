using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using RGDZY.control;

namespace RGDZY.data
{
    /// <summary>
    /// SeminarRecordHandler 的摘要说明
    /// </summary>
    public class SeminarRecordHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
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
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error: " + e.Message);
            }
        }

        public void getAllSeminarRecords(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();
            
            List<SeminarRecord> semRecList = new List<SeminarRecord>();

            try
            {
                int id = Int32.Parse(context.Request["id"]);
                var query = from semRec in dc.GetTable<SeminarRecord>()
                            where semRec.SeminarId == id
                            select semRec;
                foreach (var semRec in query)
                {
                    semRecList.Add(semRec);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            string json = Json.stringify(semRecList);
            context.Response.ContentType = "json";
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void addSeminarRecord(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            SeminarRecord SeminarRecord = Json.parse<SeminarRecord>(context.Request["parameter"]);

            try
            {
                dc.GetTable<SeminarRecord>().InsertOnSubmit(SeminarRecord);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            context.Response.ContentType = "json";
            context.Response.Write(Json.stringify(SeminarRecord));

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void editSeminarRecord(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            SeminarRecord semRec = Json.parse<SeminarRecord>(context.Request["parameter"]);

            try
            {

                var query = from sem in dc.GetTable<SeminarRecord>()
                            where sem.Id == semRec.Id
                            select sem;
                if (query.Count() > 0)
                {
                    foreach (var sem in query)
                    {
                        sem.Recorder = semRec.Recorder;
                        sem.Date = semRec.Date;
                        sem.Agenda = semRec.Agenda;
                        sem.Appendix = semRec.Appendix;
                        dc.SubmitChanges();
                    }
                }
                else
                {
                    dc.GetTable<SeminarRecord>().InsertOnSubmit(semRec);
                    dc.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var json = Json.stringify(semRec);
            context.Response.Write(json);

            DBConnectionSingleton.Instance.ReturnDBConnection(dc);
        }

        public void deleteSeminarRecord(HttpContext context)
        {
            DataContext dc = DBConnectionSingleton.Instance.BorrowDBConnection();

            SeminarRecord semRec = Json.parse<SeminarRecord>(context.Request["parameter"]);

            try
            {
                var query = from sem in dc.GetTable<SeminarRecord>()
                            where sem.Id == semRec.Id
                            select sem;
                foreach (var sem in query)
                {
                    dc.GetTable<SeminarRecord>().DeleteOnSubmit(sem);
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