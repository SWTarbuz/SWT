using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ATMIntegrationTest
{
    class BUStep10
    {
        private ITrack _track;
        private List<ITrack> _tracks;
        private IAirspace airspace;
        private List<IEvent> _events;
        private ITrackManager _tm;
        private IEventList _el;
        private WrapThat.SystemBase.IConsole _console;
        private ITrackRenderer _uut;
        private IEvent _event;

        private int _eventsRecieved;
        private TracksUpdatedEventArgs _RecievedArgs;

        [SetUp]
        public void Setup()
        {
            _track = new Track("tag",20000,20000,600,new DateTime());
            _tracks = new List<ITrack>();
            _tracks.Add(_track);
            airspace = new Airspace(10000, 90000, 10000, 90000, 500, 20000);
            _events = new List<IEvent>();
            var evnt = new EntryEvent(_track);
            //evnt.InvolvedTracks = new ITrack[1]; //Can't substitute for array of ITrack
            //evnt.InvolvedTracks[0] = _track;
            //evnt.TimeOfOccurence = _track.Timestamp;
            _events.Add(evnt);

            _eventsRecieved = 0;

            _tm = new TrackManager();
            _el = new EventList(_tm);


            _console = Substitute.For<WrapThat.SystemBase.IConsole>();

            _uut = new TrackRenderer(_tm, _el, _console);
        }

        [Test]
        public void TestHandleTrackUpdate_EventSent_ConsoleWritesLineAny()
        {
            //TracksUpdatedEventArgs sentArgs = new TracksUpdatedEventArgs(_tracks, _track);
            _tm.HandleTrack(_track, airspace);

            _console.Received().WriteLine(Arg.Any<string>());

        }

        [Test]
        public void TestHandleTrackUpdate_EventSent_ConsoleWritesLineTrack()
        {
            //TracksUpdatedEventArgs sentArgs = new TracksUpdatedEventArgs(_tracks, _track);

            _tm.HandleTrack(_track, airspace);

            _console.Received().WriteLine(Arg.Is($"track named: {_track.Tag}, located at x : {_track.XPos}, y: {_track.YPos}, altitude: {_track.Altitude}, with air speed velocity at: {_track.Velocity}, course: {_track.CompassCourse}, as of: {_track.Timestamp}"));
        }

        [Test]
        public void TestHandleEventUpdate_EventSent_ConsoleWritesLineAny()
        {
            _event = Substitute.For<SeperationEvent>(new Track("tag", 20000, 20000, 600, new DateTime()), new Track("tag2", 20000, 20000, 600, new DateTime()));

            _el.CurrEvents = Substitute.For<List<IEvent>>();
            _el.CurrEvents.Add(_event);

            _el.RaiseEventsUpdatedEvent += (o, args) =>

            _console.Received().WriteLine(Arg.Any<string>());
        }

        
    }
}
