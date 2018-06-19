using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Logging;
using vWoW.Data.Enums;

namespace vWoW.Logging
{
    public static class Logger
    {
        public static void Log(LogType logType, String logtext)
        {
            String output = "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] ";
            output = output + logtext;
            Console.WriteLine(output);
        }
    }
}
