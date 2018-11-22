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
using TransponderReceiver;

namespace ATMIntegrationTest
{
    class BUStep7
    {
        private ITransponderReceiver transponderReceiver;
        private ITrackFormatter _formatter;
        private ITrackManager _tm;
        private TransponderRecieverClient _uut;
        private int _eventsRecieved;

        [SetUp]
        public void Setup()
        {
            _eventsRecieved = 0;

            transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            _formatter = new TrackFormatter();
            _tm = new TrackManager();

            _uut = new TransponderRecieverClient(transponderReceiver, _formatter, _tm);
            transponderReceiver.TransponderDataReady += (sender, args) => _eventsRecieved++;
        }

        //TODO: fix this test to actually check something more than the fact that the given event actually happened, or remove it
        //really not sure how to test this, as this just tests that the event is sent, atm we just assign a chosen time delay, and see if events are received during this delay.
        [TestCase(2500)]
        [TestCase(3000)]
        public void TestReception_LegalValues_RecievesData(int time)
        {
            System.Threading.Thread.Sleep(time);   
            
            Assert.That(_eventsRecieved, Is.GreaterThan(0));
        }




    }
}
