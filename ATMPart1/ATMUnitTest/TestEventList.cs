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
            

            ITrack track1 = Substitute.For<Track>("1", 10, 10, 10, DateTime.MaxValue);
            ITrack track2 = Substitute.For<Track>("2", 10, 10, 10, DateTime.MaxValue);

            
            _event = Substitute.For<SeperationEvent>(track1, track2);
            //_event = new SeperationEvent(track1, track2);

            _uut.CurrEvents = Substitute.For<List<IEvent>>();
            _uut.CurrEvents.Add(_event);

            _uut.RaiseEventsUpdatedEvent += (o, args) =>
            {
                _eventArgs = args.Events;
                _eventsOccured++;
            };
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

        [Test]
        public void EndEvent_EventDoesntExists_NoChangesAreMade()
        {
            //Act
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
            ITrack track2 = Substitute.For<Track>("4", 10, 10, 10, DateTime.MinValue);

            IEvent evnt = Substitute.For<SeperationEvent>(track1, track2);
            _uut.CurrEvents.Add(evnt);

            _uut.EndEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(1));
        }

        //TODO: Add tests of EventHandlers, as these do make a new event, and that we can test for. Or in case of the timer removes an item.
        
        [Test]
        public void HandleRaiseEntryDetectedEvent_EventOccured_RaiseEventsUpdatedEventOnce()
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
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
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
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
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
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
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
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
            ITrack track1 = Substitute.For<Track>("3", 10, 10, 10, DateTime.MinValue);
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
