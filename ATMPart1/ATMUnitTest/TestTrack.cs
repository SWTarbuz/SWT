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
        [TestCase(2000, 3000, 2000, 3000, "201712122000250", "201712122001250", 45)]
        [TestCase(3000, 2000, 5000, 3000, "201712122000250", "201712122021250", 180)]
        [TestCase(3000, 2000, 5000, 3000, "201712122000250", "201712122021250", 270)]
        [TestCase(3000, 2000, 5000, 3000, "201712122000250", "201712122021250", 360)]
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
        [TestCase(2000, 3000, 2000, 3000, 2000, 3000, "201712122000250", "201712122001250", 28.8675134f)]
        [TestCase(3000, 2000, 5000, 3000, 2000, 3000, "201712122000250", "201712122021250", 180)]
        [TestCase(3000, 2000, 5000, 3000, 2000, 3000, "201712122000250", "201712122021250", 270)]
        [TestCase(3000, 2000, 5000, 3000, 2000, 3000, "201712122000250", "201712122021250", 360)]
        public void TestChangePosition_ValidValues_CalculatesCorrectVelocity(float oldX, float newX, float oldY, float newY, float oldZ, float newZ, string oldTime, string newTime, float expectedResult)
        {
            //Arrange
            string[] formats = { "yyyyMMddHHmmfff" };
            var time1 = DateTime.ParseExact(oldTime, formats[0], CultureInfo.CurrentCulture);
            var time2 = DateTime.ParseExact(newTime, formats[0], CultureInfo.CurrentCulture);

            var UUT = new Track("", oldX, oldY, oldZ, time1);

            UUT.ChangePosition(newX, newY, newZ, time2); //act

            Assert.That(UUT.velocity, Is.EqualTo(expectedResult));
        }

    }
}
