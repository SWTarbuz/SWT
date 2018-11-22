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

        private ITrack _newTrack;
        private ITrack _oldTrack;


        [SetUp]
        public void Setup()
        {
            _newTrack = Substitute.For<ITrack>();
            _newTrack.Timestamp = new DateTime(2013, 6, 1, 12, 32, 40);

            _oldTrack = Substitute.For<ITrack>();
            _oldTrack.Timestamp = new DateTime(2013, 6, 1, 12, 32, 30);

        }

        [Test]
        public void TestSeperationEvent_SeperationEventOccurs_SetsToExpectedTime()
        {
            _oldTrack.Tag = "J443";
            _newTrack.Tag = "B431";

            var se = new SeperationEvent(_newTrack, _oldTrack);

            Assert.That(se.TimeOfOccurence, Is.EqualTo(_newTrack.Timestamp));
        }

        [Test]
        public void TestSeperationEvent_SeperationEventDoesNotOccur_DoesNotSetTime()
        {
            var defaultTime = new DateTime();
            _newTrack.Tag = "J443";
            _oldTrack.Tag = "J443";

            var se = new SeperationEvent(_newTrack, _oldTrack);

            Assert.That(se.TimeOfOccurence, Is.EqualTo(defaultTime));
        }
    }
}
