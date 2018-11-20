﻿using System;
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
        private ITrackManager tm;

        [SetUp]
        public void Setup()
        {
            tm = Substitute.For<TrackManager>();

            _uut = new EventList(tm);

            ITrack track1 = Substitute.For<Track>("1", 10, 10, 10, DateTime.MaxValue);
            ITrack track2 = Substitute.For<Track>("2", 10, 10, 10, DateTime.MaxValue);

            
            _event = Substitute.For<SeperationEvent>(track1, track2);
            //_event = new SeperationEvent(track1, track2);

            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);
        }

        // TODO Test fails because substitude for seperation event is not a real seperation event so GetType() != seperationevent
        [TestCase("1", "2")]
        [TestCase("2", "1")]
        public void UpdateCurrEvent_EventExists_EventNotAdded(string tag1, string tag2)
        {
            //TODO: Consider if this makes sense, or if the design of the program needs to be redone
            //Silly arrange so GetType works as expected
            ITrack track1 = Substitute.For<Track>("1", 10, 10, 10, DateTime.MaxValue);
            ITrack track2 = Substitute.For<Track>("2", 10, 10, 10, DateTime.MaxValue);
            _event = new SeperationEvent(track1, track2);
            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);

            //Arrange
            ITrack track3 = Substitute.For<Track>(tag1, 10, 10, 10, DateTime.MinValue);
            ITrack track4 = Substitute.For<Track>(tag2, 10, 10, 10, DateTime.MinValue);

            IEvent evnt = Substitute.For<SeperationEvent>(track3, track4);

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
