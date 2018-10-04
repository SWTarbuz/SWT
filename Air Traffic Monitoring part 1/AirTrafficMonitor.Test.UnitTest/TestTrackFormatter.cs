using System;
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
        public void TestRecieveTrack_LegalValue_ReturnsTrack() //naming is not specific enough yet
        {
            var data = "";
            var time = DateTime.Now;

            var trackFormatter = Substitute.For<ITrackFormatter>();
            //tagString, xPosFloat, yPosFloat, altitudeFloat, velocityFloat, courseInt
            var returnVal = Substitute.For<Track>("tag", 3.7f, 2000.5f, 5000, time);

            trackFormatter.RecieveTrack("").Returns(returnVal);
            Assert.That(trackFormatter.RecieveTrack(data), Is.EqualTo(returnVal));
        }
    }
}
