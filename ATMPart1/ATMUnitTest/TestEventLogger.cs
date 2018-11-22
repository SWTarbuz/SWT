using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ATMPart1;
using WrapThat.SystemIO;


namespace ATMUnitTest
{
    public class TestEventLogger
    {
        private EventLogger _logger;

        [SetUp]
        public void Setup()
        {

            //EventLogger.Writer = Substitute.For<TextWriter>();
            _logger = new EventLogger();
            _logger.Writer = Substitute.For<TextWriter>();
        }

        [TestCase("Any string")]
        [TestCase("EntryEvent track: xyz, at the time of 21:11:23")]
        public void LogEventToFile_WithEventText_StreamWriterCalledWithArgEventText(string eventText)
        {
            //EventLogger.LogEventToFile(eventText);
            _logger.LogMessage(eventText);

            _logger.Writer.Received().WriteLine(Arg.Is(eventText));
            //_logger;
        }
    }
}