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

        //not really sure if this is even testing anything, except that recieveTrack can be called
        [Test]
        public void TestRecieveTrack_LegalValueInSubstiture_ReturnsTrack()
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
        public void TestRecieveTrack_validData_ReturnsExpectedTime() //format test to match the way  TestRecieveTrack_IllegalTimeString_ThrowsFormatException is set up.
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
        public void TestRecieveTrack_LegalData_ReturnsExpectedTag()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;201712122000250";

            var trackFormatter = new TrackFormatter();

            //Act
            var res = trackFormatter.RecieveTrack(data);

            Assert.That(res.tag, Is.EqualTo("tag"));
        }

        [Test]
        public void TestRecieveTrack_TooLongDataString_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;201712122000250;forMeget";

            var trackFormatter = new TrackFormatter();


            Assert.Throws<ArgumentOutOfRangeException>(() => trackFormatter.RecieveTrack(data));
        }

        [Test]
        public void TestRecieveTrack_TooShortDataString_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000";

            var trackFormatter = new TrackFormatter();


            Assert.Throws<ArgumentOutOfRangeException>(() => trackFormatter.RecieveTrack(data));
        }

        //technically really just a test of the dateTime class, as this is the one throwing the exception
        [TestCase("201713122000251")] //13th month, not legal
        [TestCase("2017121220002511")] //too long string, doens't match required format
        [TestCase("201712122400251")] //24 hrs, when hrs are defined in 0-23 Hrs.
        [TestCase("201712122061251")] //61 minutes
        [TestCase("20100122061251")] //0th month
        public void TestRecieveTrack_IllegalTimeString_ThrowsFormatException(String dateString)
        {
            //Arrange
            var data = String.Concat("tag;3.7;2000.5;5000;", dateString);

            var trackFormatter = new TrackFormatter();


            Assert.Throws<FormatException>(() => trackFormatter.RecieveTrack(data));
        }
    }
}
