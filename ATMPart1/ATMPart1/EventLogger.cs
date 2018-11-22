using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    class EventLogger
    {
        private static StreamWriter _writer; //TODO: make wrap on this so we can fake.

        public EventLogger(StreamWriter writer=null)
        {
            _writer = writer ?? new StreamWriter(@"Eventlog.txt", true);
        }

        public void LogEventToFile(string eventToLog)
        {
            _writer.WriteLine(eventToLog);

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Eventlog.txt", true))
            //{
            //    file.WriteLine(eventToLog);
            //}
        }
    }
}
