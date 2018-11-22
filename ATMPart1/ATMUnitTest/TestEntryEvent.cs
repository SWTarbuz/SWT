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
        private ITrack _track;
        private DateTime _time;
        private IEvent _uut;

        [SetUp]
        public void Setup()
        {
            _time = DateTime.MaxValue;
            _track = Substitute.For<ITrack>();
            _track.XPos = 20000;
            _track.YPos = 20000;
            _track.Altitude = 550;
            _track.Timestamp = DateTime.MaxValue;
            
            _uut = new EntryEvent(_track);
        }


        [Test]
        public void testEntryEvent_CreateEntryEvent_TimeGetsSet()
        {
            Assert.That(_uut.TimeOfOccurence, Is.EqualTo(_track.Timestamp));
        }

        [Test]
        public void testEntryEvent_CreateEntryEvent_InvolvedTracksGetsSet()
        {
            Assert.That(_uut.InvolvedTracks.Count, Is.EqualTo(1));
            Assert.That(_uut.InvolvedTracks[0].Tag, Is.EqualTo(_track.Tag));
        }

        [Test]
        public void testEntryEvent_Print_CorrectStringReturned()
        {
            Assert.That(_uut.Print, Is.EqualTo($"at the time of: {_uut.TimeOfOccurence}, the track: {_uut.InvolvedTracks[0].Tag}, Entered the Airspace"));

        }
    }
}
