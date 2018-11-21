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
        private int _margin;
        private FakeSubscriber _sub;
        private IEvent _evnt;
        private EventTimer _uut;
        private IEvent _outEvent;
        private int _eventsReceived;

        [SetUp]
        public void Setup()
        {
            _margin = 100;
            _eventsReceived = 0;
            _evnt = Substitute.For<IEvent>();

            _sub = Substitute.For<FakeSubscriber>();
        }

        //MethodUnderTest_Scenario_ExpectedBehaviour
        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(5000)]
        public void EventTimer_xTime_RaiseTimerOccuredEventAfterxTime(int time)
        {
            _uut = new EventTimer(_evnt, time);
            _uut.RaiseTimerOccuredEvent += _sub.EventSub;

            System.Threading.Thread.Sleep(time+_margin); //Adds a little bit to ensure that the timer has time to call the event

            Assert.That(_sub.Cnt, Is.EqualTo(1));
        }

        //TODO: These run perfectly fine when ran alone, but when together they fail.
        [TestCase(500)] //likely to short a time to wait.
        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(5000)]
        public void EventTimer_xTime_RaiseTimerOccuredEventNotOccuredBeforexTime(int time)
        {
            _uut = new EventTimer(_evnt, time);
            _uut.RaiseTimerOccuredEvent += _sub.EventSub;

            System.Threading.Thread.Sleep(time -_margin);
            Assert.That(_sub.Cnt, Is.EqualTo(0));
        }
    }

    //Using our own fake here due to an issue where our count on the test class had issues when running multiple tests.
    public class FakeSubscriber
    {
        public int Cnt = 0;

        //testing of event by a subscriber that we can call .Recieved on
        public void EventSub(object source, TimerForEventOccuredEventArgs e)
        {
            Cnt++;
        }
    }
}