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
    class BUStep1
    {
        private Track track;
        private DateTime time;

        private ExitEvent exitEvent;


        [SetUp]
        public void SetUp()
        {
            track = new Track("tag", 100000, 20000, 550f, time);
            time = new DateTime();

            
        }
       

        [Test]
        public void testExitEvent_CreateExitEvent_TimeGetsSet()
        {
            exitEvent = new ExitEvent(track);

            Assert.That(exitEvent.TimeOfOccurence, Is.EqualTo(track.Timestamp));
        }


        [Test]
        public void testExitEvent_CreateExitEvent_InvolvedTracksGetsSet()
        {
            exitEvent = new ExitEvent(track);

            Assert.That(exitEvent.InvolvedTracks.Count, Is.EqualTo(1));
            Assert.That(exitEvent.InvolvedTracks[0].Tag, Is.EqualTo(track.Tag));
        }


        [Test]
        public void testExitEvent_Print_CorrectStringReturned()
        {
            exitEvent = new ExitEvent(track);

            Assert.That(exitEvent.Print, Is.EqualTo($"at the time of: {exitEvent.TimeOfOccurence}, the track: {exitEvent.InvolvedTracks[0].Tag}, Left the Airspace"));
        }
    }
}
