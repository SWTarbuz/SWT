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
        private Track track;

        private TrackFormatter tf;

        [SetUp]

        public void SetUp()
        {

            tf = new TrackFormatter();
        }


    }
}
