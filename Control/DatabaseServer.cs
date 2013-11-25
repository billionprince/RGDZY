using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Timers;

namespace RGDZY.Control
{
    public abstract class ObjectPool
    {
        //Last Checkout time of any object from the pool.
        private long lastCheckOut;

        //Hashtable of the check-out objects.
        private static Hashtable locked;

        //Hashtable of available objects
        private static Hashtable unlocked;

        //Clean-Up interval
        internal static long GARBAGE_INTERVAL = 90 * 1000; //90 seconds
        static ObjectPool()
        {
            locked = Hashtable.Synchronized(new Hashtable());
            unlocked = Hashtable.Synchronized(new Hashtable());
        }

        internal ObjectPool()
        {
            lastCheckOut = DateTime.Now.Ticks;

            //Create a Time to track the expired objects for cleanup.
            Timer aTimer = new Timer();
            aTimer.Enabled = true;
            aTimer.Interval = GARBAGE_INTERVAL;
            aTimer.Elapsed += new ElapsedEventHandler(CollectGarbage);
        }

        protected abstract object Create();

        protected abstract bool Validate(object o);

        protected abstract void Expire(object o);

        internal object GetObjectFromPool()
        {
            long now = DateTime.Now.Ticks;
            lastCheckOut = now;
            object o = null;

            lock (this)
            {
                try
                {
                    foreach (DictionaryEntry myEntry in unlocked)
                    {
                        o = myEntry.Key;
                        unlocked.Remove(o);
                        if (Validate(o))
                        {
                            locked.Add(o, now);
                            return o;
                        }
                        else
                        {
                            Expire(o);
                            o = null;
                        }
                    }
                }
                catch (Exception) { }
                o = Create();
                locked.Add(o, now);
            }
            return o;
        }

        internal void ReturnObjectToPool(object o)
        {
            if (o != null)
            {
                lock (this)
                {
                    locked.Remove(o);
                    unlocked.Add(o, DateTime.Now.Ticks);
                }
            }
        }

        private void CollectGarbage(object sender, ElapsedEventArgs ea)
        {
            lock (this)
            {
                object o;
                long now = DateTime.Now.Ticks;
                IDictionaryEnumerator e = unlocked.GetEnumerator();

                try
                {
                    while (e.MoveNext())
                    {
                        o = e.Key;

                        if ((now - (long)unlocked[o]) > GARBAGE_INTERVAL)
                        {
                            unlocked.Remove(o);
                            Expire(o);
                            o = null;
                        }
                    }
                }
                catch (Exception) { }
            }
        }
    }

    public sealed class DBConnectionSingletion : ObjectPool
    {
        private DBConnectionSingletion() { }

        public static readonly DBConnectionSingletion Instance =
            new DBConnectionSingletion();

        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        protected override object Create()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                conn.Close();
            }
            catch (SqlException) { }
        }

        public SqlConnection BorrowDBConnection()
        {
            try
            {
                return (SqlConnection)base.GetObjectFromPool();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnDBConnection(SqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        }
    }
}