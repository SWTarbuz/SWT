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

        [SetUp]
        public void Setup()
        {

        }

        [TestCase(90001, 10000, 500)]
        [TestCase(90000, 90001, 500)]
        [TestCase(90000, 90000, 499)]
        [TestCase(90000, 9999, 500)]
        [TestCase(9999, 10000, 500)]
        [TestCase(10000, 10000, 20001)]
        public void testHandleTrack_TrackOutsideAirspace_NothingChanged(int x, int y, int z)
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", x, y, z, time);
            var tm = new TrackManager();

            tm.HandleTrack(trak, airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_AddsTrack()
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, time);
            var tm = new TrackManager();

            tm.HandleTrack(trak,airspace);

            Assert.That(tm.Tracks.ElementAt(0).altitude, Is.EqualTo(trak.altitude));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_EntryEventGetsRaised()
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, time);
            var tm = new TrackManager();

            var called = false;
            tm.RaiseEntryDetectedEvent += (sender, args) => called = true;
            tm.HandleTrack(trak, airspace);

            Assert.IsTrue(called);
        }

        [Test] 
        public void testHandleTrack_TrackEntersAirspace_ListCountIs1()
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, time);
            var tm = new TrackManager();

            tm.HandleTrack(trak, airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(1));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_RemovesTrack() 
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, time);
            var trak2 = Substitute.For<Track>("tag", 20000, 20000, 400, time);
            var tm = Substitute.For<ITrackManager>();

            tm.HandleTrack(trak, airspace);
            tm.HandleTrack(trak2, airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_ExitEventGetsRaised()
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, time);
            var trak2 = Substitute.For<Track>("tag", 20000, 20000, 400, time);
            var tm = new TrackManager();

            tm.HandleTrack(trak, airspace);
            var called = false;
            tm.RaiseExitDetectedEvent += (sender, args) => called = true;
            tm.HandleTrack(trak2, airspace);

            Assert.IsTrue(called);
        }

        [Test]
        public void testHandleTrack_TrackMovesWithinAirspace_UpdatesTrack() 
        {
            var time = new DateTime();
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 20000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, time);
            var trak2 = Substitute.For<Track>("tag", 20000, 24000, 550, DateTime.Now);
            var tm = new TrackManager();

            tm.HandleTrack(trak, airspace);
            tm.HandleTrack(trak2, airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(1));
            Assert.That(tm.Tracks[0].yPos, Is.EqualTo(24000));
        }


        //TODO: Add test of track being updated with new position -- I think this has been done now

        //TODO: add test that checks that 'OnRaiseTrackUpdatedEvent' is being raised correctly.
    }
}
