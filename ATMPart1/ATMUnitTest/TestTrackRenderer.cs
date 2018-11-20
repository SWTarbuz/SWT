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

namespace ATMUnitTest
{
    public class TestTrackRenderer
    {
        private ITrack _track;
        private List<ITrack> _tracks;

        private ITrackManager _tm;
        private IEventList _el;
        private ITrackRenderer _uut;

        private int _eventsRecieved;
        private TracksUpdatedEventArgs _RecievedArgs;

        [SetUp]
        public void Setup()
        {
            _track = Substitute.For<ITrack>();
            _tracks = Substitute.For<List<ITrack>>();
            _tracks.Add(_track);

            _eventsRecieved = 0;

            _tm = Substitute.For<ITrackManager>();
            _el = Substitute.For<IEventList>();
            _uut = new TrackRenderer(_tm, _el);
        }

        //TODO: Can this even be tested? As everything is private, and it doesn't send any events out.
        //[Test]
        //public void TestHandleTrackUpdate_EventSent_RecievedEvent()
        //{
        //    TracksUpdatedEventArgs sentArgs = new TracksUpdatedEventArgs(_tracks, _track);

        //    _tm.RaiseTracksUpdatedEvent += Raise.EventWith(_tm, sentArgs);

            
        //    Assert.That();
        //}
    }
}