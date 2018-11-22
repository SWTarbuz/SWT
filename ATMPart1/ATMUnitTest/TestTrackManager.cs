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
    class TestTrackManager
    {
        private IAirspace _airspace;
        private ITrackManager _uut;

        private ITrack _trackInside;
        private ITrack _trackOutside;

        [SetUp]
        public void Setup()
        {
           _airspace = Substitute.For<IAirspace>();
           _uut = new TrackManager();

            _trackInside= Substitute.For<ITrack>();
            _trackInside.Tag = "1";
            _trackInside.YPos = 10;
            _trackOutside = Substitute.For<ITrack>();
            _trackOutside.Tag = "1";
            _trackOutside.YPos = 24000;

            _airspace.IsWithinBounds(Arg.Is(_trackInside)).Returns(true);
            _airspace.IsWithinBounds(Arg.Is(_trackOutside)).Returns(false);
        }

        //TODO: not really sure this is the correct way of testing this, I think we should make airspace.IsWithingBounds return true/false, and use that to get coverage.
        [Test]
        public void testHandleTrack_TrackOutsideAirspace_NothingChanged()
        {
            _uut.HandleTrack(_trackOutside, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_AddsTrack()
        {
            _uut.HandleTrack(_trackInside, _airspace);

            Assert.That(_uut.Tracks.ElementAt(0).Altitude, Is.EqualTo(_trackInside.Altitude));
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_EntryEventGetsRaised()
        {
            var called = false;
            _uut.RaiseEntryDetectedEvent += (sender, args) => called = true;
            _uut.HandleTrack(_trackInside, _airspace);

            Assert.IsTrue(called);
        }

        [Test]
        public void testHandleTrack_TrackEntersAirspace_UpdateTracksEventGetsRaised()
        {
            var called = false;
            _uut.RaiseTracksUpdatedEvent += (sender, args) => called = true;
            _uut.HandleTrack(_trackInside, _airspace);

            Assert.IsTrue(called);
        }

        [Test] 
        public void testHandleTrack_TrackEntersAirspace_ListCountIs1()
        {
            _uut.HandleTrack(_trackInside, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(1));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_RemovesTrack() 
        {
            _uut.HandleTrack(_trackInside, _airspace);
            _uut.HandleTrack(_trackOutside, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_ExitEventGetsRaised()
        {
            _uut.HandleTrack(_trackInside, _airspace);
            var called = false;
            _uut.RaiseExitDetectedEvent += (sender, args) => called = true;
            _uut.HandleTrack(_trackOutside, _airspace);

            Assert.IsTrue(called);
        }

        [Test]
        public void testHandleTrack_TrackLeavesAirspace_UpdateTracksEventGetsRaised()
        {

            _uut.HandleTrack(_trackInside, _airspace);
            var called = false;
            _uut.RaiseTracksUpdatedEvent += (sender, args) => called = true;
            _uut.HandleTrack(_trackOutside, _airspace);

            Assert.IsTrue(called);
        }

        [Test]
        public void testHandleTrack_TrackMovesWithinAirspace_ChangePositionCalled()
        {
            var called = false;
            _trackInside.When(x => x.ChangePosition(Arg.Any<float>(), Arg.Any<float>(), Arg.Any<float>(), Arg.Any<DateTime>())).Do(x => called = true);

            ITrack track2 = Substitute.For<ITrack>();
            track2.Tag = "1";
            track2.YPos = 24000;

            _airspace.IsWithinBounds(Arg.Is(track2)).Returns(true);

            _uut.HandleTrack(_trackInside, _airspace);
            _uut.HandleTrack(track2, _airspace);

            Assert.That(_uut.Tracks.Count, Is.EqualTo(1));
            Assert.That(called, Is.EqualTo(true));
        }
    }
}
