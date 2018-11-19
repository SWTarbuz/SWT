using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATMPart1
{
    class Program
    {

        static void Main(string[] args)
        {
            ITransponderReceiver reciever = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();

            ITrackManager tm = new TrackManager();
            IEventList evntList = new SeperationEventList();

            ITrackRenderer renderer = new TrackRenderer(tm, evntList);
            ISeperationEventDetector evntDetector = new SeperationEventDetector(evntList, tm);

            TransponderRecieverClient client = new TransponderRecieverClient(reciever, new TrackFormatter(), tm);

            while (true)
            {
                

            }
        }
    }
}
