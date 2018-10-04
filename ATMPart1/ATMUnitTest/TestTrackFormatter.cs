using System;
using System.Globalization;
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
        public void TestRecieveTrack_LegalValueInSubstiture_ReturnsTrack() //naming is not specific enough yet
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
        public void TestRecieveTrack_validData_ReturnsExpectedTime() //naming is not specific enough yet
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

        [Test]
        public void TestRecieveTrack_LegalData_ReturnsExpectedTag() //naming is not specific enough yet
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;201712122000250";

            var trackFormatter = new TrackFormatter();

            //Act
            var res = trackFormatter.RecieveTrack(data);

            Assert.That(res.tag, Is.EqualTo("tag"));
        }

        [Test]
        public void TestRecieveTrack_TooLongDataString_Throws() //naming is not specific enough yet
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;201712122000250;forMeget";

            var trackFormatter = new TrackFormatter();


            Assert.Throws<ArgumentOutOfRangeException>(() => trackFormatter.RecieveTrack(data));
        }

        [Test]
        public void TestRecieveTrack_TooShortDataString_Throws() //naming is not specific enough yet
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000";

            var trackFormatter = new TrackFormatter();


            Assert.Throws<ArgumentOutOfRangeException>(() => trackFormatter.RecieveTrack(data));
        }
    }
}
