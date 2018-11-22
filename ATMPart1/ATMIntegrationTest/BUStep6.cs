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
        private Track track;
        private EntryEvent _evnt;
        //private ExitEvent _outEvent;
        //private int _eventsReceived;
        private DateTime time;
        private bool called;

        private EventTimer _uut;

        [SetUp]
        public void SetUp()
        {
            time = new DateTime();
            _margin = 500;
            //_eventsReceived = 0;
            track = new Track("tag", 20000, 20000, 550f, time);
            _evnt = new EntryEvent(track);
            called = false;

        }

        [TestCase(2000)]
        [TestCase(5000)]
        public void EventTimer_xTime_RaiseTimerOccuredEventAfterxTime(int testTime)
        {
            _uut = new EventTimer(_evnt, testTime);

            _uut.RaiseTimerOccuredEvent += (sender, args) => called = true;

            System.Threading.Thread.Sleep(testTime + _margin); //Adds a little bit to ensure that the timer has time to call the event

            Assert.IsTrue(called);
        }

        //TODO: These run perfectly fine when ran alone, but when together they fail.
        [TestCase(2000)]
        [TestCase(5000)]
        public void EventTimer_xTime_RaiseTimerOccuredEventNotOccuredBeforexTime(int testTime)
        {
            _uut = new EventTimer(_evnt, testTime);

            _uut.RaiseTimerOccuredEvent += (sender, args) => called = true;

            System.Threading.Thread.Sleep(testTime - _margin);

            Assert.IsFalse(called);
        }
    }
}
