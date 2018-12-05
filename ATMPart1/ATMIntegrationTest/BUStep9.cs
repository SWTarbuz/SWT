using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMIntegrationTest
{
    class BUStep9
    {
        private List<ITrack> _tracks;
        private EventList _el;
        private TrackManager _tm;
        private Track track;

        private SeperationEventDetector _uut;

        [SetUp]
        public void Setup()
        {
            _tracks = new List<ITrack>();

            //track = new Track("tag",0,0,0,new DateTime());

            //track.Tag = "0";
            //track.XPos = 0;
            //track.YPos = 0;
            //track.Altitude = 0;


            //_tracks.Add(track);
            _tm = new TrackManager();
            _el = new EventList(_tm);
            
            _uut = new SeperationEventDetector(_el, _tm);

        }


        /// <summary>
        /// Checks that every track is checked for an event with the newly added.
        /// </summary>
        [Test]
        public void TestUpdateEvents_EventDetection_SeperationEventOccurs()
        {

            var testTrack = new Track("tag", 25000, 25000, 600, new DateTime());
            var testTrack2 = new Track("tag2", 25000, 25000, 600, new DateTime());

            SeperationEvent se = new SeperationEvent(testTrack,testTrack2);

            _tracks.Add(testTrack); //keeps same values to ensure that event occurs
            _tracks.Add(testTrack2);

            _uut.UpdateEvents(_tracks[0], _tracks);

            Assert.AreEqual(_el.CurrEvents[0].TimeOfOccurence,se.TimeOfOccurence);
     
        }

 
    }
}
