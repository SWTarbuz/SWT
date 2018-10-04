using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ATMUnitTest
{
    
    class TestTrack
    {

        [SetUp]
        public void Setup()
        {

        }
        //       oldX  newX  oldY   newY    oldTime             newTime         expectedResult
        [TestCase(0,   1000,  0,    1000, "201712122000250", "201712122001250", 45)] //Q1 45* = 45*
        [TestCase(0,   2000,  0,    1000, "201712122000250", "201712122021250", 63)] //Q1 63* = 63*
        [TestCase(0,      0,  1000,    0, "201712122000250", "201712122021250", 180)] //Q4 90* = 180*
        [TestCase(1000,   0,  0,       0, "201712122000250", "201712122021250", 270)] //Q3 90* = 270*
        [TestCase(0,      0,  0,    1000, "201712122000250", "201712122021250", 360)] //Q2 90* = 360*
        public void TestChangePosition_ValidValues_CalculatesCorrectCompassCourse(float oldX, float newX, float oldY, float newY, string oldTime, string newTime, int expectedResult)
        {
            //Arrange
            string[] formats = { "yyyyMMddHHmmfff" };
            var time1 = DateTime.ParseExact(oldTime, formats[0], CultureInfo.CurrentCulture);
            var time2 = DateTime.ParseExact(newTime, formats[0], CultureInfo.CurrentCulture);

            var UUT = new Track("", oldX, oldY, 0, time1);

            UUT.ChangePosition(newX, newY, 0, time2); //act

            Assert.That(UUT.compassCourse, Is.EqualTo(expectedResult));

        }

        //       oldX  newX  oldY   newY  oldZ  newZ     oldTime         newTime       expectedResult
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122001250", 28.8675134f)] //positive vals 1 min
        [TestCase(3000, 2000, 3000, 2000, 3000, 2000, "201712122000250", "201712122100250", 0.481125f)] //negative vals & 1 hour
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122100250", 0.481125f)] //positive vals & 1 hour
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122000251", 1732050.807569f)] //1000'th of a second positive vals
        [TestCase(2000, 2000, 2000, 2000, 2000, 2000, "201712122000250", "201712122000251", 0f)] //no location difference, 1000th of a second
        //[TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122000251", 1732050.807569f)] //no time difference positive vals (expects time rounding)
        public void TestChangePosition_ValidValues_CalculatesCorrectVelocity(float oldX, float newX, float oldY, float newY, float oldZ, float newZ, string oldTime, string newTime, float expectedResult)
        {
            //Arrange
            string[] formats = { "yyyyMMddHHmmfff" };
            var time1 = DateTime.ParseExact(oldTime, formats[0], CultureInfo.CurrentCulture);
            var time2 = DateTime.ParseExact(newTime, formats[0], CultureInfo.CurrentCulture);

            var UUT = new Track("", oldX, oldY, oldZ, time1);

            UUT.ChangePosition(newX, newY, newZ, time2); //act

            //rounds to 6 decimals to ensure that the result of the test isn't a false negative due to rounding differences
            Assert.That(Math.Round(UUT.velocity, 6), Is.EqualTo(Math.Round(expectedResult, 6)));
        }

    }
}
