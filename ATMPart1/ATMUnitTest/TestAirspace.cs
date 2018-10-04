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

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestWithinBounds_TrackWithinBounds_ReturnsTrue()
        {
          
            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 2000);
           
            var returnVal = Substitute.For<Track>("tag", 20000, 20000, 550, time);

            Assert.That(airspace.IsWithinBounds(returnVal), Is.EqualTo(true));
        }

        [Test]
        public void TestWithinBounds_TrackWithinBounds_ReturnsFalse()
        {

            var time = DateTime.Now;
            var airspace = Substitute.For<Airspace>(10000, 90000, 10000, 90000, 500, 2000);

            var returnVal = Substitute.For<Track>("tag", 20000, 20000, 400, time);

            Assert.That(airspace.IsWithinBounds(returnVal), Is.EqualTo(false));
        }
    }
}