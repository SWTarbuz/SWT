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
    class TestExitEvent
    {

        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void testExitEvent_CreateExitEvent_TimeGetsSet()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 100000, 20000, 550f, time);

            IEvent exitEvent = new ExitEvent(trak);

            Assert.That(exitEvent.TimeOfOccurence, Is.EqualTo(trak.timestamp));
        }

        [Test]
        public void testExitEvent_CreateExitEvent_InvolvedTracksGetsSet()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 100000, 20000, 550f, time);

            IEvent exitEvent = new ExitEvent(trak);

            Assert.That(exitEvent.InvolvedTracks.Count, Is.EqualTo(1));
            Assert.That(exitEvent.InvolvedTracks[0].tag, Is.EqualTo(trak.tag));
        }

        [Test]
        public void testExitEvent_Print_CorrectStringReturned()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 90001, 20000, 550f, time);

            IEvent exitEvent = new ExitEvent(trak);

            Assert.That(exitEvent.Print, Is.EqualTo($"at the time of: {exitEvent.TimeOfOccurence}, the track: {exitEvent.InvolvedTracks[0].tag}, Left the Airspace"));

        }
    }
}