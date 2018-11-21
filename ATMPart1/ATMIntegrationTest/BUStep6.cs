using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace ATMIntegrationTest
{
    class BUStep6
    {
        private int _margin;
        //private FakeSubscriber _sub;
        private EntryEvent _evnt;
        private EventTimer _uut;
        private ExitEvent _outEvent;
        private int _eventsReceived;

        [SetUp]
        public void SetUp()
        {
            _evnt = new EntryEvent();

        }
    }
}
