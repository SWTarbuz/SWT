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
    class TestEventList
    {
        private IEventList _uut;
        private IEvent _event;
        private ITrackManager _tm;

        private int _eventsOccured;
        private List<IEvent> _eventArgs;

        [SetUp]
        public void Setup()
        {
            _eventsOccured = 0;
            _tm = Substitute.For<ITrackManager>();

            _uut = new EventList(_tm);
            
            //TODO: loop and use list or other collection of tracks
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "1";
            track1.Timestamp = DateTime.MaxValue;

            ITrack track2 = Substitute.For<ITrack>();
            track2.Tag = "2";
            track2.Timestamp = DateTime.MaxValue;
          
            _event = Substitute.For<SeperationEvent>(track1, track2);

            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);

            _uut.RaiseEventsUpdatedEvent += (o, args) =>
            {
                _eventArgs = args.Events;
                _eventsOccured++;
            };
        }

        //TODO: can't substitute for IEvent as we are using GetType in the code, and thus the type won't match
        //TODO: Either live with not substituting or update code to inject the type we test for.
        [TestCase("1", "2")]
        [TestCase("2", "1")]
        public void UpdateCurrEvent_EventExists_EventNotAdded(string tag1, string tag2)
        {
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = tag1;
            ITrack track2 = Substitute.For<ITrack>();
            track2.Tag = tag2;
            _event = new SeperationEvent(track1, track2);
            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2); //bad stuff

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(1));
        }

        [TestCase("1", "2")]
        [TestCase("2", "1")]
        public void UpdateCurrEvent_EventExists_OriginalEventKeptUnchanged(string tag1, string tag2)
        {
            //Arrange
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "1";
            track1.Timestamp = DateTime.MinValue;

            ITrack track2 = Substitute.For<ITrack>();
            track2.Tag = "2";
            track2.Timestamp = DateTime.MinValue;

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);

            //Act
            _uut.UpdateCurrEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.EqualTo(_event));
        }

        [TestCase("1", "3")]
        [TestCase("3", "1")]
        [TestCase("3", "2")]
        [TestCase("3", "4")]
        public void UpdateCurrEvent_EventDoesntExist_EventAdded(string tag1, string tag2)
        {
            //Arrange
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = tag1;
            track1.XPos = int.Parse(tag1) * 12;
            ITrack track2 = Substitute.For<ITrack>();
            track1.Tag = tag2;
            track1.XPos = int.Parse(tag2) * 9;


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

        [Test]
        public void EndEvent_EventDoesntExists_NoChangesAreMade()
        {
            //Act
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            ITrack track2 = Substitute.For<ITrack>();
            track1.Tag = "4";

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);
            _uut.CurrEvents.Add(evnt);

            _uut.EndEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void HandleRaiseEntryDetectedEvent_EventOccured_RaiseEventsUpdatedEventOnce()
        {
            //Arrange
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            List<ITrack> tl = Substitute.For<List<ITrack>>();
            tl.Add(track1);

            var args = new TracksUpdatedEventArgs(tl, track1);
            _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That(_eventsOccured, Is.EqualTo(1));
        }
        [Test]
        public void HandleRaiseEntryDetectedEvent_EventOccured_EntryEventAdded()
        {
            //Arrange
            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            List<ITrack> tl = Substitute.For<List<ITrack>>();
            tl.Add(track1);

            var args = new TracksUpdatedEventArgs(tl, track1);
            _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.TypeOf<EntryEvent>());
        }

        [Test]
        public void HandleRaiseExitDetectedEvent_EventOccured_RaiseEventsUpdatedEventOnce()
        {
            //Arrange
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            List<ITrack> tl = Substitute.For<List<ITrack>>();
            tl.Add(track1);

            var args = new TracksUpdatedEventArgs(tl, track1);
            _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That(_eventsOccured, Is.EqualTo(1));
        }

        [Test]
        public void HandleRaiseExitDetectedEvent_EventOccured_ExitEventAdded()
        {
            //Arrange
            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            List<ITrack> tl = Substitute.For<List<ITrack>>();
            tl.Add(track1);

            var args = new TracksUpdatedEventArgs(tl, track1);
            _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That(_uut.CurrEvents[0], Is.TypeOf<ExitEvent>());
        }

        [Test]
        public void HandleRaiseEntryAndExitDetectedEvent_EventOccured_RaiseEventsUpdatedEventTwice()
        {
            //Arrange
            ITrack track1 = Substitute.For<ITrack>();
            track1.Tag = "3";
            List<ITrack> tl = Substitute.For<List<ITrack>>();
            tl.Add(track1);

            var args = new TracksUpdatedEventArgs(tl, track1);
            _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);
            _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That(_eventsOccured, Is.EqualTo(2));
        }

    }
}
