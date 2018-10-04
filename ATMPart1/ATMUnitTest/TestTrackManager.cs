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
        public void testHandleTrack_TrackLeavesAirspace_RemovesTrack()
        {
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 2000);
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550, time);
            var trak2 = Substitute.For<Track>("tag", 20000, 20000, 400, time);
            var tm = Substitute.For<ITrackManager>();

            tm.HandleTrack(trak,airspace);
            tm.HandleTrack(trak2,airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }

    }
}
