using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMUnitTest
{
    class TestSeperationEventList
    {
        private ISeperationEventList _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new SeperationEventList();
        }

        //MethodUnderTest_Scenario_Behaviour
        [Test]
        public void EndEvent_EventExists_RemovesEvent()
        {
            //Arrange
            ITrack track1 = Substitute.For<Track>("", 10, 10, 10, DateTime.Now);
            ITrack track2 = Substitute.For<Track>("", 10, 10, 10, DateTime.Now);

            ISeperationEvent evnt = Substitute.For<SeperationEvent>(track1, track2);
            _uut.CurrEvents = Substitute.For<List<ISeperationEvent>>();
            _uut.CurrEvents.Add(evnt);

            //Act
            _uut.EndEvent(evnt);

            //Assert
            Assert.That(_uut.CurrEvents.Count, Is.EqualTo(0));

        }
    }
}
