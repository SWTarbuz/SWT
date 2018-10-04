using System;
using ATMPart1;
using Castle.DynamicProxy.Generators;
using NSubstitute;
using NUnit.Framework;

namespace ATMUnitTest
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
        public void TestRecieveTrack_LegalValue_ReturnsMatchingTrack() //naming is not specific enough yet
        {
            var data = "tag;3.7;2000.5;5000;20151006213456789";
            var time = DateTime.Now;

            var trackFormatter = new TrackFormatter();

            //tagString, xPosFloat, yPosFloat, altitudeFloat, timestamp
            var returnVal = Substitute.For<Track>("tag", 3.7f, 2000.5f, 5000, time); //sets up our return value
            trackFormatter.RecieveTrack("").Returns(returnVal);

            Assert.That(trackFormatter.RecieveTrack(data), Is.EqualTo(returnVal)); //act and assert
        }
    }
}
