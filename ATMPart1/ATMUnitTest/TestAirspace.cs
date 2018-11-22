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

        [TestCase(10000, 20000, 1000)] //x low
        [TestCase(90000, 20000, 1000)] //x high
        [TestCase(20000, 10000, 1000)] //y low
        [TestCase(20000, 90000, 1000)] //y high
        [TestCase(20000, 20000, 500)] //z low
        [TestCase(20000, 10000, 1000)] //z high
        public void TestWithinBounds_TrackWithinBounds_ReturnsTrue(int x, int y, int z)
        {         
            var track = Substitute.For<ITrack>();
            track.XPos = x;
            track.YPos = y;
            track.Altitude = z;

            Assert.That(_uut.IsWithinBounds(track), Is.EqualTo(true));
        }


        [TestCase(9999 , 20000, 1000)] //x low
        [TestCase(90001, 20000, 1000)] //x high
        [TestCase(20000, 9999 , 1000)] //y low
        [TestCase(20000, 90001, 1000)] //y high
        [TestCase(20000, 20000, 499 )] //z low
        [TestCase(20000, 10000, 2001)] //z high
        public void TestWithinBounds_TrackOutsideBounds_ReturnsFalse(int x, int y, int z)
        {
            var track = Substitute.For<ITrack>();
            track.XPos = x;
            track.YPos = y;
            track.Altitude = z;

            Assert.That(_uut.IsWithinBounds(track), Is.EqualTo(false));
        }
    }
}