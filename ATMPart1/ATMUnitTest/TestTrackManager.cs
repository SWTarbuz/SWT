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

            tm.HandleTrack(trak,airspace);
            tm.HandleTrack(trak2,airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }

    }
}
