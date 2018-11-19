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
            ITrackRenderer renderer = new TrackRenderer(tm);
            ISeperationEventDetector evntDetector = new SeperationEventDetector(new SeperationEventList(), renderer, tm);

            TransponderRecieverClient client = new TransponderRecieverClient(reciever, new TrackFormatter(), tm);

            while (true)
            {
                

            }
        }
    }
}
