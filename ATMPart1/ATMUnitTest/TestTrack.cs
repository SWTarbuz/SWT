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
        //doesn't have test to see if ChangePosition changes the properties, except for course and velocity
        //also lacks tests to see that illegal values are handled correctly

        /// <summary>
        /// Test of calls to CalcCourse from ChangePosition
        /// </summary>
        //         dX     dY    expectedResult
        [TestCase(1000,   1000, 45)] //Q1 45* = 45*
        [TestCase(2000,   1000, 63)] //Q1 63* = 63*
        [TestCase(0,     -1000, 180)] //Q4 90* = 180*
        [TestCase(-1000,   0,   270)] //Q3 90* = 270*
        [TestCase(0,      1000, 0)] //Q2 90* = 360* = 0* due to this being the same and design says 0* to 359*
        public void TestChangePosition_CalcCourse_CalculatesExpectedValue(float dX, float dY, int expectedResult)
        {
            DateTime time = DateTime.Now.Add(new TimeSpan(0, 1, 0, 0));

            var UUT = new Track("", 0, 0, 0, DateTime.Now);

            UUT.ChangePosition(dX, dY, 0, time); //act

            Assert.That(UUT.compassCourse, Is.EqualTo(expectedResult));

        }

        /// <summary>
        /// Test of calls to CalcVelocity from ChangePosition
        /// </summary>
        //       oldX  newX  oldY   newY  oldZ  newZ     oldTime         newTime       expectedResult
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122001250", 28.8675134f)] //positive vals 1 min
        [TestCase(3000, 2000, 3000, 2000, 3000, 2000, "201712122000250", "201712122100250", 0.481125f)] //negative vals & 1 hour
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122100250", 0.481125f)] //positive vals & 1 hour
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122000251", 1732050.807569f)] //1000'th of a second positive vals
        //[TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122000251", 1732050.807569f)] //no time difference positive vals (expects time rounding)
        public void TestChangePosition_CalcVelocity_CalculatesExpectedValue(float oldX, float newX, float oldY, float newY, float oldZ, float newZ, string oldTime, string newTime, float expectedResult)
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

        [Test]
        public void TestChangePosition_NoChangeInTime_ThrowsArgumentException()
        {
            var time = DateTime.Now;

            var UUT = new Track("", 0, 0, 0, time);

            //rounds to 6 decimals to ensure that the result of the test isn't a false negative due to rounding differences
            Assert.Throws<ArgumentException>(() => UUT.ChangePosition(0, 0, 0, time));
        }

    }
}
