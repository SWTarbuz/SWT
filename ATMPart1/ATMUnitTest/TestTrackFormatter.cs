using System;
using System.Globalization;
using Air_Traffic_Monitoring_part_1;
using Castle.DynamicProxy.Generators;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.UnitTest
{
    class TestTrackFormatter
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestRecieveTrackFake_LegalValue_ReturnsTrack() //naming is not specific enough yet
        {
            var data = "";
            var time = DateTime.Now;

            var trackFormatter = Substitute.For<ITrackFormatter>();

            //tagString, xPosFloat, yPosFloat, altitudeFloat, timestamp
            var returnVal = Substitute.For<Track>("tag", 3.7f, 2000.5f, 5000, time); //sets up our return value
            trackFormatter.RecieveTrack("").Returns(returnVal);

            Assert.That(trackFormatter.RecieveTrack(data), Is.EqualTo(returnVal)); //act and assert
        }

        [Test]
        public void TestRecieveTrack_LegalValue_ReturnsExpectedTime() //naming is not specific enough yet
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;201712122000250";
            string[] formats = { "yyyyMMddHHmmfff" };
            var time = DateTime.ParseExact("201712122000250", formats[0], CultureInfo.CurrentCulture); ;

            var trackFormatter = new TrackFormatter();

            //Act
            var res = trackFormatter.RecieveTrack(data);

            Assert.That(res.timestamp, Is.EqualTo(time));
        }
    }
}
