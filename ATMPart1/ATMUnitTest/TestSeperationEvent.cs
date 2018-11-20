using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ATMUnitTest
{
    class TestSeperationEvent
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestSeperationEvent_SeperationEventOccurs_SetsToExpectedTime()
        {
            var time = DateTime.Now;
            DateTime date1 = new DateTime(2013, 6, 1, 12, 32, 30);
            var nTrack = Substitute.For<Track>("B342", 20000, 20000, 550f, date1);
            var oTrack = Substitute.For<Track>("J443", 20000, 20000, 550f, time);

            var se = new SeperationEvent(nTrack, oTrack);

            Assert.That(se.timeOfOccurence, Is.EqualTo(date1));
        }

        [Test]
        public void TestSeperationEvent_SeperationEventDoesNotOccur_DoesNotSetTime()
        {
            //Just make the tags the same
            var time = DateTime.Now;
            var defaultTime = new DateTime();
            DateTime date1 = new DateTime(2013, 6, 1, 12, 32, 30);
            var nTrack = Substitute.For<Track>("J443", 20000, 20000, 550f, date1);
            var oTrack = Substitute.For<Track>("J443", 20000, 20000, 550f, time);

            var se = new SeperationEvent(nTrack, oTrack);

            Assert.That(se.timeOfOccurence, Is.EqualTo(defaultTime));
        }
    }
}
