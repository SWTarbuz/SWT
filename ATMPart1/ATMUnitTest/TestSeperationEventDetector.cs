using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
namespace ATMUnitTest
{
    class TestSeperationEventDetector
    {

        [SetUp]
        public void Setup()
        {
            List<ITrack> tracks = new List<ITrack>();

            for (int i = 0; i <= 1; i++)
            {
                tracks.Add(new Track(i.ToString(), i * 1000, i * 1000, i * 500, DateTime.Now));
            }
        }

        //[TestCase(0)] //first track in the list is the one that is to be called as changed
        public void TestUpdateEvents_EventDetectionWithXTracks_DetectsExpectedAmountOfEvents(int indexOfChangedTrack)
        {
            var eList = Substitute.For<ISeperationEventList>();
            var rend = Substitute.For<ITrackRenderer>();

            SeperationEventDetector UUT = new SeperationEventDetector(eList, rend);

            //setup what fakes do to test


            //Assert that eList was called correct amount of times
        }
    }
}
