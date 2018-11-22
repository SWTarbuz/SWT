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
        private Track track;
        private ITrackFormatter _formatter;
        private ITrackManager _tm;
        private int _eventsRecieved;
        private ITransponderReceiver _fakeTransponderReceiver;
        private DateTime time;
        private Airspace airspace;

        private TransponderRecieverClient _uut;

        [SetUp]
        public void Setup()
        {
            _eventsRecieved = 0;
            //time = new DateTime(2015, 10, 06, 21, 34, 56, 789);
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _formatter = new TrackFormatter();
            _tm = new TrackManager();
            airspace = new Airspace(10000, 90000, 10000, 90000, 500, 2000);
            //track = new Track("ATR423",39045,12932,14000,time);

            _uut = new TransponderRecieverClient(_fakeTransponderReceiver, _formatter, _tm);
            _fakeTransponderReceiver.TransponderDataReady += (sender, args) => _eventsRecieved++;
        }

        //TODO: fix this test to actually check something more than the fact that the given event actually happened, or remove it
        //really not sure how to test this, as this just tests that the event is sent, atm we just assign a chosen time delay, and see if events are received during this delay.
        [Test]
        public void TestReception_LegalValues_RecievesData()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady +=
                Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //_uut.Received().ReceiverOnTransponderDataReady(Arg.Is(_fakeTransponderReceiver), Arg.Any<RawTransponderDataEventArgs>()); //TODO: Does it make sense to use a substitute for uut to do this, and thus check that the event can be recieved?
            Assert.That(_eventsRecieved, Is.EqualTo(1));
        }

        /// <summary>
        /// Checks that the Reciever calls the formatter for every single string in the recieved event. (Indirectly checks that event is received aswell)
        /// </summary>
        /// <param name="x"></param>
        [Test]
        public void ReceiverOnTransponderDataReady_ReceiveTrack_TrackIsReceived()
        {
            string[] formats = { "yyyyMMddHHmmssfff" };
            track = (Track)_formatter.RecieveTrack("ATR423;39045;12932;14000;20151006213456789");
            time = DateTime.ParseExact("20151006213456789", formats[0], CultureInfo.CurrentCulture); ;

            Assert.That(track.Timestamp, Is.EqualTo(time));
        }

        [Test]
        public void ReceiverOnTransponderDataReady_HandleTrack_TrackIsOutOfAirspace()
        {
            track = new Track("B837", 0, 0, 0, DateTime.Now);
            _tm.HandleTrack(track, airspace);

            Assert.That(_tm.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReceiverOnTransponderDataReady_HandleTrack_EntersAirSpace()
        {
            track = new Track("B837", 0, 0, 0, DateTime.Now);
            var track2 = new Track("B837", 20000, 20000, 700, DateTime.Now);
            _tm.HandleTrack(track, airspace);
            Assert.That(_tm.Tracks.Count, Is.EqualTo(0));
            track = track2;
            _tm.HandleTrack(track, airspace);
            Assert.That(_tm.Tracks.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReceiverOnTransponderDataReady_HandleTrack_ExitsAirSpace()
        {
            track = new Track("B837", 20000, 20000, 700, DateTime.Now);
            var track2 = new Track("B837", 0, 0, 0, DateTime.Now);
            _tm.HandleTrack(track, airspace);
            Assert.That(_tm.Tracks.Count, Is.EqualTo(1));
            track = track2;
            _tm.HandleTrack(track, airspace);
            Assert.That(_tm.Tracks.Count, Is.EqualTo(0));
        }

    }
}
