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
    [TestFixture]
    public class BUStep1
    {

        private Track track;
        private Airspace airspace;
        private SeperationEvent seperationEvent;

        private TrackManager tm;
        private TrackFormatter tf;
        private SeperationEventList seList;

        [SetUp]
        public void SetUp()
        {           
            track = new Track("", 0, 0, 0, DateTime.Now);
            airspace = new Airspace(10000, 90000, 10000, 90000, 500, 2000);

        }
    }
}
