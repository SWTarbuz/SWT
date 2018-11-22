using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace ATMIntegrationTest
{
    [TestFixture]
    public class BUStep4
    {
        private Track track;
        private Airspace airspace;

        private TrackManager tm;


        [SetUp]
        public void SetUp()
        {           

            airspace = new Airspace(10000, 90000, 10000, 90000, 500, 2000);

            tm = new TrackManager();
        }




        [Test]
        public void TrackManager_HandleTrack_OutOfAirSpace()
        {
            track = new Track("B837", 0, 0, 0, DateTime.Now);
            tm.HandleTrack(track, airspace);

            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }
    
        [Test]
        public void TrackManager_HandleTrack_EntersAirSpace()
        {
            track = new Track("B837", 0, 0, 0, DateTime.Now);
            var track2 = new Track("B837", 20000, 20000, 700, DateTime.Now);
            tm.HandleTrack(track, airspace);
            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
            track = track2;
            tm.HandleTrack(track, airspace);
            Assert.That(tm.Tracks.Count, Is.EqualTo(1));
        }


        [Test]
        public void TrackManager_HandleTrack_ExitsAirSpace()
        {
            track = new Track("B837", 20000, 20000, 700, DateTime.Now);
            var track2 = new Track("B837", 0, 0, 0, DateTime.Now);
            tm.HandleTrack(track, airspace);
            Assert.That(tm.Tracks.Count, Is.EqualTo(1));
            track = track2;
            tm.HandleTrack(track, airspace);
            Assert.That(tm.Tracks.Count, Is.EqualTo(0));
        }

    }
}
