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
        private ISeperationEventList _uut;
        private ISeperationEvent _event;

        [SetUp]
        public void Setup()
        {
            _uut = new SeperationEventList();

            ITrack track1 = Substitute.For<Track>("", 10, 10, 10, DateTime.MaxValue);
            ITrack track2 = Substitute.For<Track>("", 10, 10, 10, DateTime.MaxValue);

            _event = Substitute.For<SeperationEvent>(track1, track2);
            _uut.CurrEvents = Substitute.For<List<ISeperationEvent>>();
            _uut.CurrEvents.Add(_event);
        }

        [Test]
        public void UpdateCurrEvent_EventExists_OriginalEventKeptUnchanged()
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>("", 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>("", 10, 10, 10, DateTime.MinValue);

            ISeperationEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.EqualTo(_event));
            //Assert.That(_uut.CurrEvents[0].InvolvedTracks[0].timestamp, Is.EqualTo(DateTime.MaxValue));
        }

        [Test]
        public void UpdateCurrEvent_EventDoesntExist_EventAdded()
        {
            //Arrange
            _uut.CurrEvents = Substitute.For<List<ISeperationEvent>>();

            ITrack track1 = Substitute.For<Track>("", 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>("", 10, 10, 10, DateTime.MinValue);

            ISeperationEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.EqualTo(evnt));
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
