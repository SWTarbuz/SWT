using System;
using System.Globalization;
using ATMPart1;
using Castle.DynamicProxy.Generators;
using NSubstitute;
using NUnit.Framework;

namespace ATMUnitTest

{
    class TestAirspace
    {
        private IAirspace _uut;
        private DateTime _time;

        [SetUp]
        public void Setup()
        {
            _time = DateTime.MaxValue;
            _uut = new Airspace(10000, 90000, 10000, 90000, 500, 2000);
        }

        [Test]
        public void TestWithinBounds_TrackWithinBounds_ReturnsTrue()
        {         
            var track = Substitute.For<ITrack>();
            track.xPos = 20000;
            track.yPos = 20000;
            track.altitude = 550;

            Assert.That(_uut.IsWithinBounds(track), Is.EqualTo(true));
        }

        [Test]
        public void TestWithinBounds_TrackOutsideBounds_ReturnsFalse()
        {
            var track = Substitute.For<ITrack>();
            track.xPos = 20000;
            track.yPos = 20000;
            track.altitude = 400;

            Assert.That(_uut.IsWithinBounds(track), Is.EqualTo(false));
        }
    }
}