using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMIntegrationTest
{
    class BUStep8
    {
        private EventList _uut;
        private IEvent _event;
        private TrackManager _tm;

        private int _eventsOccured;
        private List<IEvent> _eventArgs;

        [SetUp]
        public void Setup()
        {
            _eventsOccured = 0;
            _tm = new TrackManager();

            _uut = new EventList(_tm);

            //TODO: loop and use list or other collection of tracks
            Track track1 = new Track("1",25000,25000,600,new DateTime());

            Track track2 = new Track("2", 25000, 25000, 600, new DateTime());

            _event = new SeperationEvent(track1, track2);

            _uut.CurrEvents = new List<IEvent>();
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
            Track track1 = new Track(tag1, 25000, 25000, 600, new DateTime());

            Track track2 = new Track(tag2, 25000, 25000, 600, new DateTime());

            _event = new SeperationEvent(track1, track2);

            _uut.CurrEvents = new List<IEvent>();
            _uut.CurrEvents.Add(_event);

            IEvent evnt = new SeperationEvent(track1, track2); //bad stuff

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
            Track track1 = new Track(tag1, 25000, 25000, 600, new DateTime());

            Track track2 = new Track(tag2, 25000, 25000, 600, new DateTime());

            IEvent evnt = new SeperationEvent(track1, track2);

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
            Track track1 = new Track(tag1, 25000, 25000, 600, new DateTime());

            Track track2 = new Track(tag2, 25000, 25000, 600, new DateTime());


            IEvent evnt = new SeperationEvent(track1, track2);

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
            Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

            Track track2 = new Track("4", 25000, 25000, 600, new DateTime());

            IEvent evnt = new SeperationEvent(track1, track2);
            _uut.CurrEvents.Add(evnt);

            _uut.EndEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(1));
        }

        //[Test]
        //public void HandleRaiseEntryDetectedEvent_EventOccured_RaiseEventsUpdatedEventOnce()
        //{
        //    //Arrange
        //    Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

        //    List<ITrack> tl = new List<ITrack>();
        //    tl.Add(track1);

        //    var args = new TracksUpdatedEventArgs(tl, track1);
        //    _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);

        //    //Assert
        //    Assert.That(_eventsOccured, Is.EqualTo(1));
        //}
        //[Test]
        //public void HandleRaiseEntryDetectedEvent_EventOccured_EntryEventAdded()
        //{
        //    //Arrange
        //    _uut.CurrEvents = new List<IEvent>();
        //    Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

        //    List<ITrack> tl = new List<ITrack>();
        //    tl.Add(track1);

        //    var args = new TracksUpdatedEventArgs(tl, track1);
        //    _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);

        //    //Assert
        //    Assert.That(_uut.CurrEvents[0], Is.TypeOf<EntryEvent>());
        //}

        //[Test]
        //public void HandleRaiseExitDetectedEvent_EventOccured_RaiseEventsUpdatedEventOnce()
        //{
        //    //Arrange
        //    Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

        //    List<ITrack> tl = new List<ITrack>();
        //    tl.Add(track1);

        //    var args = new TracksUpdatedEventArgs(tl, track1);
        //    _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

        //    //Assert
        //    Assert.That(_eventsOccured, Is.EqualTo(1));
        //}

        //[Test]
        //public void HandleRaiseExitDetectedEvent_EventOccured_ExitEventAdded()
        //{
        //    //Arrange
        //    _uut.CurrEvents = new List<IEvent>();
        //    Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

        //    List<ITrack> tl = new List<ITrack>();
        //    tl.Add(track1);

        //    var args = new TracksUpdatedEventArgs(tl, track1);
        //    _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

        //    //Assert
        //    Assert.That(_uut.CurrEvents[0], Is.TypeOf<ExitEvent>());
        //}

        //[Test]
        //public void HandleRaiseEntryAndExitDetectedEvent_EventOccured_RaiseEventsUpdatedEventTwice()
        //{
        //    //Arrange
        //    Track track1 = new Track("3", 25000, 25000, 600, new DateTime());

        //    List<ITrack> tl = new List<ITrack>();
        //    tl.Add(track1);

        //    var args = new TracksUpdatedEventArgs(tl, track1);
        //    _tm.RaiseEntryDetectedEvent += Raise.EventWith(args);
        //    _tm.RaiseExitDetectedEvent += Raise.EventWith(args);

        //    //Assert
        //    Assert.That(_eventsOccured, Is.EqualTo(2));
        //}
    }
}
