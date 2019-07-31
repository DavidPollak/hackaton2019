using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageReco_17
{
    public static class Logger
    {
        private static log4net.ILog Log { get; set; }


        static Logger()
        {
            Log = log4net.LogManager.GetLogger("Icash");
        }

        public static void Debug(object msg)
        {
            Console.WriteLine("Debug: " + msg);
            Log.Debug(msg);
        }

        public static void Info(object msg)
        {
            if (!msg.ToString().Contains("HeartBeatEvent"))
            {
                Console.WriteLine("Info: " + msg);
            }
            Log.Info(msg);
        }

        public static void Warn(object msg)
        {
            Console.WriteLine("Warn " + msg);
            Log.Warn(msg);
        }

        public static void Error(object msg)
        {
            Console.WriteLine(msg);
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Console.WriteLine("Error " + msg + "  " + ex.Message.ToString());
            Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Console.WriteLine("Error " + ex.Message.ToString());
            Log.Error(ex.Message, ex);
        }

        public static void Fatal(object msg)
        {
            Console.WriteLine("Fatal " + msg);
            Log.Fatal(msg);
        }
        public static void Trace(string message, Exception exception)
        {
            Log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                log4net.Core.Level.Trace, message, exception);
        }
        public static void Trace(string message)
        {
            Log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                log4net.Core.Level.Trace, message, null);
        }
    }
}
