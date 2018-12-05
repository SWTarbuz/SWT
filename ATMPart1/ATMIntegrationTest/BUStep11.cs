using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace ATMIntegrationTest
{
    class BUStep11
    {
        private ITrackManager tm;
        private TransponderRecieverClient client;
        private ITransponderReceiver receiver;
        private IEventList el;
        private ITrackRenderer tr;
        private ISeperationEventDetector evntDetector;
        private WrapThat.SystemBase.IConsole _console;
        private ITrackFormatter _tf;

        [SetUp]
        public void SetUp()
        {
            receiver = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();
            tm = new TrackManager();
            el = new EventList(tm);
            _tf = new TrackFormatter();
            evntDetector = new SeperationEventDetector(el, tm);
            client = new TransponderRecieverClient(receiver, _tf, tm);
            _console = Substitute.For<WrapThat.SystemBase.IConsole>();

            tr = new TrackRenderer(tm, el,_console);
            
        }


        //TODO: This test is not sufficient, but it is the only test available that we can detect, which can confirm some validity of this integration step.
        [Test]
        public void TestHandleEventUpdate_EventSent_ConsoleWritesLineAny()
        {
            ITrack track = null;
            tm.RaiseTracksUpdatedEvent += (o, args) => { track = args.UpdatedTrack; };

            //el.RaiseEventsUpdatedEvent += (o, args) => { str = args.Events[0].Print(); };
            System.Threading.Thread.Sleep(15000);
            //if (str != "")          
            //_console.Received().WriteLine(Arg.Is($"track named: {track.Tag}, located at x : {track.XPos}, y: {track.YPos}, altitude: {track.Altitude}, with air speed velocity at: {track.Velocity}, course: {track.CompassCourse}, as of: {track.Timestamp}"));
            Assert.IsNotNull(track);
        }



    }
}
