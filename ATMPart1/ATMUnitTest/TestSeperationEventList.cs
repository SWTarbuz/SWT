using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMUnitTest
{
    class TestSeperationEventList
    {
        private IEventList _uut;
        private IEvent _event;

        [SetUp]
        public void Setup()
        {
            _uut = new EventList();

            ITrack track1 = Substitute.For<Track>("1", 10, 10, 10, DateTime.MaxValue);
            ITrack track2 = Substitute.For<Track>("2", 10, 10, 10, DateTime.MaxValue);

            _event = Substitute.For<SeperationEvent>(track1, track2);
            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);
        }

        [TestCase("1", "2")]
        [TestCase("2", "1")]
        public void UpdateCurrEvent_EventExists_EventNotAdded(string tag1, string tag2)
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>(tag1, 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>(tag2, 10, 10, 10, DateTime.MinValue);

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(1));
            //Assert.That(_uut.CurrEvents[0].InvolvedTracks[0].timestamp, Is.EqualTo(DateTime.MaxValue));
        }

        [TestCase("1", "2")]
        [TestCase("2", "1")]
        public void UpdateCurrEvent_EventExists_OriginalEventKeptUnchanged(string tag1, string tag2)
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>(tag1, 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>(tag2, 10, 10, 10, DateTime.MinValue);

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.EqualTo(_event));
            //Assert.That(_uut.CurrEvents[0].InvolvedTracks[0].timestamp, Is.EqualTo(DateTime.MaxValue));
        }

        [TestCase("1", "3")]
        [TestCase("3", "1")]
        [TestCase("3", "2")]
        [TestCase("3", "4")]
        public void UpdateCurrEvent_EventDoesntExist_EventAdded(string tag1, string tag2)
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>(tag1, 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>(tag2, 10, 10, 10, DateTime.MinValue);

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents[1], Is.EqualTo(evnt)); //TODO: Consider if testing for amount of events makes more sense
        }

        //MethodUnderTest_Scenario_Behaviour
        [Test]
        public void EndEvent_EventExists_RemovesEvent()
        {
            //Act
            _uut.EndEvent(_event);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(0));
        }
    }
}
