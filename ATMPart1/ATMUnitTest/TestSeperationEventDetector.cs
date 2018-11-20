﻿using System;
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

            _tracks.Add(Substitute.For<Track>("0", 0, 0, 0, DateTime.Now));

            _el = Substitute.For<IEventList>();
            _tm = Substitute.For<ITrackManager>();

            _uut = new SeperationEventDetector(_el, _tm);

        }


        /// <summary>
        /// Due to not changing position between tracks every one should according to specification detect an event with the updated track. Thus devolving to a test that detects if the function calls down to the list as expected
        /// </summary>
        [TestCase(2)] //2 tracks = 1 check
        [TestCase(3)] //2 tracks = 2 check
        [TestCase(1)] //1 track = 0 checks
        public void TestUpdateEvents_EventDetectionWithXTracks_ChecksCorrectAmountOfTimes(int trackCount)
        {
            var checkCnt = 0;

            for (int i = 0; i < trackCount; i++)
            {
                _tracks.Add(new Track(i.ToString(), 0, 0, 0, DateTime.Now)); //keeps same values to ensure that even occurs
            }

            //sets up fake to count up variable, thus eliminating dependency
            _el.When(x => x.UpdateCurrEvent(Arg.Any<IEvent>()))
                .Do(x=> checkCnt++);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(trackCount-1, checkCnt);
            //Assert that eList was called correct amount of times
        }

        /// <summary>
        /// Test of the logic that decides if an event should occur, if "TestUpdateEvents_EventDetectionWithXTracks_ChecksCorrectAmountOfTimes" has errors this will give invalid results
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

            _tracks.Add(Substitute.For<Track>("1", xDist, yDist, zDist, DateTime.Now));

            //sets up fake to count up variable, thus eliminating dependency
            _el.When(x => x.UpdateCurrEvent(Arg.Any<IEvent>()))
                .Do(x => DidEventOccur = true);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(expectedResult, DidEventOccur);
            //Assert that eList was called correct amount of times
        }

        [TestCase(3000, 3999, 299, true)] //just within bounds 
        [TestCase(3000, 3999, 300, false)] //just outside vertical bound
        [TestCase(3000, 4000, 299, false)] //just outside horizontal bound
        public void TestUpdateEvents_EventDetectionWith2TracksWithDistances_CallsSeperationEventList(float xDist, float yDist, float zDist, bool expectedResult)
        {
            var DidEventOccur = false; //technically this will give the correct result, if the above tests work, but uses expected to make independent from this.
            var updatedList = false;

            _tracks.Add(Substitute.For<Track>("1", xDist, yDist, zDist, DateTime.Now));

            //sets up fake to count up variable, thus eliminating dependency
            _el.When(e => e.UpdateCurrEvent(Arg.Any<IEvent>())).Do(e => updatedList = true);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(expectedResult, updatedList);
            //Assert that renderer is called if the list was updated
        }
    }
}
