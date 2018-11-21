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

namespace ATMIntegrationTest
{
    class BUStep2
    {
        private Track track;
        
        private TrackFormatter tf;

        [SetUp]

        public void SetUp()
        {
            tf = new TrackFormatter();

        }

        [Test]

        public void TestRecieveTrack_validData_ReturnsExpectedTime() //format test to match the way  TestRecieveTrack_IllegalTimeString_ThrowsFormatException is set up.
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;20171212200012250";
            string[] formats = { "yyyyMMddHHmmssfff" };
            var time = DateTime.ParseExact("20171212200012250", formats[0], CultureInfo.CurrentCulture); ;

            //Act  
            track = (Track)tf.RecieveTrack(data);

            Assert.That(track.timestamp, Is.EqualTo(time));
        }

        [Test]
        public void TestRecieveTrack_LegalData_ReturnsExpectedTag()
        {
            //Arrange
            var data = "tag;3.7;2000.5;5000;20171212200012250";

            //Act
            track = (Track)tf.RecieveTrack(data);

            Assert.That(track.tag, Is.EqualTo("tag"));
        }



    }
}
