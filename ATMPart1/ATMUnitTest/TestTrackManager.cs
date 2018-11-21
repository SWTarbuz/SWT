using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ATMPart1;

namespace ATMUnitTest
{
    class TestTrackManager
    {
        private IAirspace _airspace;
        private ITrackManager _uut;

        [SetUp]
        public void Setup()
        {
           _airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            _uut = new TrackManager();
        }

        [TestCase(90001, 10000, 500)]
        [TestCase(90000, 90001, 500)]
        [TestCase(90000, 90000, 499)]
        [TestCase(90000, 9999, 500)]
        [TestCase(9999, 10000, 500)]
        [TestCase(10000, 10000, 20001)]
        public void testHandleTrack_TrackOutsideAirspace_NothingChanged(int x, int y, int z)
        {
            var trak = Substitute.For<Track>("tag", x, y, z, DateTime.Now);

            _uut.HandleTrack(trak, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_AddsTrack()
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, DateTime.Now);

            _uut.HandleTrack(trak, _airspace);

            Assert.That(_uut.Tracks.ElementAt(0).altitude, Is.EqualTo(trak.altitude));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_EntryEventGetsRaised()
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, DateTime.Now);

            var called = false;
            _uut.RaiseEntryDetectedEvent += (sender, args) => called = true;
            _uut.HandleTrack(trak, _airspace);

            Assert.IsTrue(called);
        }

        [Test] 
        public void testHandleTrack_TrackEntersAirspace_ListCountIs1()
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, DateTime.Now);

            _uut.HandleTrack(trak, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(1));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_RemovesTrack() 
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, DateTime.Now);
            var trak2 = Substitute.For<Track>("tag", 20000, 20000, 400, DateTime.Now);

            _uut.HandleTrack(trak, _airspace);
            _uut.HandleTrack(trak2, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_ExitEventGetsRaised()
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, DateTime.Now);
            var trak2 = Substitute.For<Track>("tag", 20000, 20000, 400, DateTime.Now);

            _uut.HandleTrack(trak, _airspace);
            var called = false;
            _uut.RaiseExitDetectedEvent += (sender, args) => called = true;
            _uut.HandleTrack(trak2, _airspace);

            Assert.IsTrue(called);
        }

        [Test]
        public void testHandleTrack_TrackMovesWithinAirspace_UpdatesTrack() 
        {
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, DateTime.MaxValue);
            var trak2 = Substitute.For<Track>("tag", 20000, 24000, 550, DateTime.MinValue);

            _uut.HandleTrack(trak, _airspace);
            _uut.HandleTrack(trak2, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(1));
            Assert.That(_uut.Tracks[0].yPos, Is.EqualTo(24000));
        }


        //TODO: Add test of track being updated with new position -- I think this has been done now

        //TODO: add test that checks that 'OnRaiseTrackUpdatedEvent' is being raised correctly.
    }
}
