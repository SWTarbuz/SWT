using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
namespace ATMIntegrationTest
{
    class BUStep2
    {

        private Track oTrack;
        private Track nTrack;
        private DateTime time;

        private SeperationEvent seperationEvent;
        [SetUp]
        public void Setup()
        {
            time = new DateTime();
            oTrack = new Track("J443", 20000, 20000, 550f, time);
        }

        [Test]
        public void TestSeperationEvent_SeperationEventOccurs_SetsToExpectedTime()
        {

            DateTime date1 = new DateTime(2013, 6, 1, 12, 32, 30);
            nTrack = new Track("B342", 20000, 20000, 550f, date1);
 
            seperationEvent = new SeperationEvent(nTrack, oTrack);

            Assert.That(seperationEvent.TimeOfOccurence, Is.EqualTo(date1));
        }

        [Test]
        public void TestSeperationEvent_SeperationEventDoesNotOccur_DoesNotSetTime()
        {
            //Just make the tags the same

            DateTime date1 = new DateTime(2013, 6, 1, 12, 32, 30);
            nTrack = new Track("J443", 20000, 20000, 550f, date1);


            seperationEvent = new SeperationEvent(nTrack, oTrack);

            Assert.That(seperationEvent.TimeOfOccurence, Is.EqualTo(time));
        }
    }
}
