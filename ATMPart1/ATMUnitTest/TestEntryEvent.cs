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
    class TestEntryEvent
    {

        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void testEntryEvent_CreateEntryEvent_TimeGetsSet()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, time);

            IEvent entryEvent = new EntryEvent(trak);

            Assert.That(entryEvent.TimeOfOccurence, Is.EqualTo(trak.timestamp));
        }

        [Test]
        public void testEntryEvent_CreateEntryEvent_InvolvedTracksGetsSet()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, time);

            IEvent entryEvent = new EntryEvent(trak);

            Assert.That(entryEvent.InvolvedTracks.Count, Is.EqualTo(1));
            Assert.That(entryEvent.InvolvedTracks[0].tag, Is.EqualTo(trak.tag));
        }

        [Test]
        public void testEntryEvent_Print_CorrectStringReturned()
        {
            var time = DateTime.Now;
            var trak = Substitute.For<Track>("tag", 20000, 20000, 550f, time);

            IEvent entryEvent = new EntryEvent(trak);

            Assert.That(entryEvent.Print, Is.EqualTo($"at the time of: {entryEvent.TimeOfOccurence}, the track: {entryEvent.InvolvedTracks[0].tag}, Entered the Airspace"));

        }
    }
}
