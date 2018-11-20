using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ATMPart1;

namespace ATMUnitTest
{
    //TODO: consider injection of the timer, as it is a dependency
    public class TestEventTimer
    {
        private IEvent _evnt;
        private EventTimer _uut;
        private IEvent _outEvent;
        private int _eventsReceived;

        [SetUp]
        public void Setup()
        {
            _eventsReceived = 0;
            _evnt = Substitute.For<IEvent>();
        }

        //MethodUnderTest_Scenario_ExpectedBehaviour
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(5000)]
        public void EventTimer_xTime_RaiseTimerOccuredEventAfterxTime(int time)
        {
            _uut = new EventTimer(_evnt, time);
            _uut.RaiseTimerOccuredEvent += (o, args) =>
            {
                _outEvent = args.Evnt;
                ++_eventsReceived;
            };

            System.Threading.Thread.Sleep(time+20); //Adds a little bit to ensure that the timer has time to call the event
            Assert.That(_eventsReceived, Is.EqualTo(1));
        }
    }
}