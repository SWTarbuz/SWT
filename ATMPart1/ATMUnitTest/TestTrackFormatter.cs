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
        public void TestRecieveTrack_validData_ReturnsExpectedTime() //format test to match the way  TestRecieveTrack_IllegalTimeString_ThrowsFormatException is set up.
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;20171212200012250";
            string[] formats = { "yyyyMMddHHmmssfff" };
            var time = DateTime.ParseExact("20171212200012250", formats[0], CultureInfo.CurrentCulture); ;

            var trackFormatter = new TrackFormatter();

            //Act
            var res = trackFormatter.RecieveTrack(data);

            Assert.That(res.timestamp, Is.EqualTo(time));
        }

        [Test]
        public void TestRecieveTrack_LegalData_ReturnsExpectedTag()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;20171212200012250";

            var trackFormatter = new TrackFormatter();

            //Act
            var res = trackFormatter.RecieveTrack(data);

            Assert.That(res.tag, Is.EqualTo("tag"));
        }

        [Test]
        public void TestRecieveTrack_TooLongDataString_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;20171212200012250;forMeget";

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
        [TestCase("20171312200012251")] //13th month, not legal
        [TestCase("201712122000122511")] //too long string, doens't match required format
        [TestCase("20171212240012251")] //24 hrs, when hrs are defined in 0-23 Hrs.
        [TestCase("20171212206112251")] //61 minutes
        [TestCase("2010012206112251")] //0th month
        [TestCase("20100122061251")] //To short a string
        public void TestRecieveTrack_IllegalTimeString_ThrowsFormatException(String dateString)
        {
            //Arrange
            var data = String.Concat("tag;3.7;2000.5;5000;", dateString);

            var trackFormatter = new TrackFormatter();


            Assert.Throws<FormatException>(() => trackFormatter.RecieveTrack(data));
        }
    }
}
