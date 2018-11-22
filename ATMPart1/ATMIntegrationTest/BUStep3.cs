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
    class BUStep3
    {
        private DateTime time;
        private Track track;

        private EntryEvent entryEvent;

        [SetUp]
        public void Setup()
        {
            time = DateTime.Now;
            track = new Track("tag", 20000, 20000, 550f, time);
        }


        [Test]
        public void testEntryEvent_CreateEntryEvent_TimeGetsSet()
        {
            entryEvent = new EntryEvent(track);

            Assert.That(entryEvent.TimeOfOccurence, Is.EqualTo(track.timestamp));
        }

        [Test]
        public void testEntryEvent_CreateEntryEvent_InvolvedTracksGetsSet()
        {
            entryEvent = new EntryEvent(track);

            Assert.That(entryEvent.InvolvedTracks.Count, Is.EqualTo(1));
            Assert.That(entryEvent.InvolvedTracks[0].tag, Is.EqualTo(track.tag));
        }

        [Test]
        public void testEntryEvent_Print_CorrectStringReturned()
        {
            entryEvent = new EntryEvent(track);

            Assert.That(entryEvent.Print, Is.EqualTo($"at the time of: {entryEvent.TimeOfOccurence}, the track: {entryEvent.InvolvedTracks[0].tag}, Entered the Airspace"));

        }
    }
}
