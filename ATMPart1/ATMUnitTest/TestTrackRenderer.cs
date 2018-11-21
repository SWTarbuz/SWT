﻿using System;
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

namespace ATMUnitTest
{
    public class TestTrackRenderer
    {
        private ITrack _track;
        private List<ITrack> _tracks;

        private List<IEvent> _events;
        private ITrackManager _tm;
        private IEventList _el;
        private WrapThat.SystemBase.IConsole _console;
        private ITrackRenderer _uut;

        private int _eventsRecieved;
        private TracksUpdatedEventArgs _RecievedArgs;

        [SetUp]
        public void Setup()
        {
            _track = Substitute.For<ITrack>();
            _tracks = Substitute.For<List<ITrack>>();
            _tracks.Add(_track);

            _events = Substitute.For<List<IEvent>>();
            var evnt = Substitute.For<IEvent>();
            evnt.InvolvedTracks = new ITrack[1]; //Can't substitute for aray of ITrack
            evnt.InvolvedTracks[0] = _track;
            evnt.timeOfOccurence = _track.timestamp;
            _events.Add(evnt);

            _eventsRecieved = 0;

            _tm = Substitute.For<ITrackManager>();
            _el = Substitute.For<IEventList>();
            

            _console = Substitute.For<WrapThat.SystemBase.IConsole>();

            _uut = new TrackRenderer(_tm, _el, _console);
        }

        [Test]
        public void TestHandleTrackUpdate_EventSent_ConsoleWritesLineAny()
        {
            TracksUpdatedEventArgs sentArgs = new TracksUpdatedEventArgs(_tracks, _track);

            _tm.RaiseTracksUpdatedEvent += Raise.EventWith(_tm, sentArgs);

            _console.Received().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void TestHandleTrackUpdate_EventSent_ConsoleWritesLineTrack()
        {
            TracksUpdatedEventArgs sentArgs = new TracksUpdatedEventArgs(_tracks, _track);

            _tm.RaiseTracksUpdatedEvent += Raise.EventWith(_tm, sentArgs);

            _console.Received().WriteLine(Arg.Is($"track named: {_track.tag}, located at x : {_track.xPos}, y: {_track.yPos}, altitude: {_track.altitude}, with air speed velocity at: {_track.velocity}, course: {_track.compassCourse}, as of: {_track.timestamp}"));
        }

        [Test]
        public void TestHandleEventUpdate_EventSent_ConsoleWritesLineAny()
        {
            RaiseEventsUpdatedEventArgs args = new RaiseEventsUpdatedEventArgs(_events);

            _el.RaiseEventsUpdatedEvent += Raise.EventWith(args);

            _console.Received().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void TestHandleEventUpdate_EventSent_ConsoleWritesLineEvent()
        {
            RaiseEventsUpdatedEventArgs args = new RaiseEventsUpdatedEventArgs(_events);

            _el.RaiseEventsUpdatedEvent += Raise.EventWith(args);

            _console.Received().WriteLine(Arg.Is(_events[0].Print()));
        }

        [Test]
        public void TestHandleEventUpdate_EventNotSent_ConsoleDoesntWriteAnything()
        {
            _console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        //TODO: add test of multiple tracks being received.
    }
}