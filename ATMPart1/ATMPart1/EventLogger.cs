using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

[assembly: InternalsVisibleTo("ATMUnitTest")] //Makes this visible/public to ATMUnitTest

namespace ATMPart1
{
    public class EventLogger : ILogger
    {
        //TODO: Maybe an issue to expose this, but somehow the Writer needs to be exposed for us to fake it.
        public TextWriter Writer;

        public EventLogger()
        {
            Writer = new StreamWriter(@"Eventlog.txt", true);
        }

        public void LogMessage(string eventToLog)
        {
            Writer.WriteLine(eventToLog);
            Writer.Flush();
        }
    }

    //public class EventLogger : ILogger
    //{
    //    private StreamWriter streamWriter;
    //    public EventLogger()
    //    {
    //        streamWriter = new StreamWriter(@"Eventlog.txt", true);
    //    }
    //    public void LogMessage(string message)
    //    {
    //        streamWriter.WriteLine(message);
    //    }
    //}
}
