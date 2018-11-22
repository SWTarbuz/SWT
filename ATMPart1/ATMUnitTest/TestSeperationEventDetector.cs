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
    class TestSeperationEventDetector
    {
        private List<ITrack> _tracks;
        private IEventList _el;
        private ITrackManager _tm;

        private ISeperationEventDetector _uut;

        [SetUp]
        public void Setup()
        {
            _tracks = Substitute.For<List<ITrack>>();

            var track = Substitute.For<ITrack>();
            track.Tag = "0";
            track.XPos = 0;
            track.YPos = 0;
            track.Altitude = 0;


            _tracks.Add(track);

            _el = Substitute.For<IEventList>();
            _tm = Substitute.For<ITrackManager>();

            _uut = new SeperationEventDetector(_el, _tm);

        }


        /// <summary>
        /// Checks that every track is checked for an event with the newly added.
        /// </summary>
        [TestCase(1)] //2 tracks = 1 check
        [TestCase(2)] //2 tracks = 2 check
        [TestCase(3)] //3 tracks = 3 checks
        public void TestUpdateEvents_EventDetectionWithXTracks_ChecksCorrectAmountOfTimes(int trackCount)
        {
            var checkCnt = 0;

            for (int i = 0; i < trackCount - 1; i++)
            {
                var track = Substitute.For<ITrack>();

                _tracks.Add(track); //keeps same values to ensure that even occurs
            }

            //sets up fake to count up variable, thus eliminating dependency
            _el.When(x => x.UpdateCurrEvent(Arg.Any<IEvent>()))
                .Do(x=> checkCnt++);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(trackCount-1, checkCnt);
            //Assert that eList was called correct amount of times
        }

        /// <summary>
        /// Test of the logic in CompareTracks.
        /// </summary>
        [TestCase(3000, 3999, 299, true)] //just withing bounds both horizontally and vertically
        [TestCase(3000, 3999, 300, false)] //just outside bounds vertical, withing horizontally
        [TestCase(3000, 4000, 299, false)] //just outside bounds horizontal, withing vertical
        [TestCase(3000, 4000, 300, false)] //just outside bounds horizontal and vertical
        [TestCase(0,       0, 299, true)] //just inside vertical, no horizontal
        [TestCase(3000, 3999, 0, true)] //just inside horizontal, no vertical
        public void TestUpdateEvents_EventDetectionWith2TracksWithDistances_DetectsExpectedAmountOfEvents(float xDist, float yDist, float zDist, bool expectedResult)
        {
            var DidEventOccur = false;

            var track = Substitute.For<ITrack>();
            track.XPos = xDist;
            track.YPos = yDist;
            track.Altitude = zDist;
            track.Tag = "1";

            _tracks.Add(track);

            _el.When(x => x.UpdateCurrEvent(Arg.Any<IEvent>()))
                .Do(x => DidEventOccur = true);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(expectedResult, DidEventOccur);
        }

        [TestCase(3000, 3999, 299, true)] //just within bounds 
        [TestCase(3000, 3999, 300, false)] //just outside vertical bound
        [TestCase(3000, 4000, 299, false)] //just outside horizontal bound
        public void TestUpdateEvents_EventDetectionWith2TracksWithDistances_CallsSeperationEventList(float xDist, float yDist, float zDist, bool expectedResult)
        {
            var DidEventOccur = false; //technically this will give the correct result, if the above tests work, but uses expected to make independent from this.
            var updatedList = false;

            var track = Substitute.For<ITrack>();
            track.XPos = xDist;
            track.YPos = yDist;
            track.Altitude = zDist;
            track.Tag = "1";

            _tracks.Add(track);

            //sets up fake to count up variable, thus eliminating dependency
            _el.When(e => e.UpdateCurrEvent(Arg.Any<IEvent>())).Do(e => updatedList = true);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(expectedResult, updatedList);
            //Assert that renderer is called if the list was updated
        }
    }
}
