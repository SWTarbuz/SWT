using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    static class EventLogger
    {
        public static void LogEventToFile(string eventToLog)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Eventlog.txt", true))
            {
                file.WriteLine(eventToLog);
            }
        }
    }
}
