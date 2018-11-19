using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using TransponderReceiver;

namespace ATMPart1
{
    public class TransponderRecieverClient
    {
        private ITransponderReceiver _receiver;

        private ITrackFormatter _formatter;
        private ITrackManager _manager;
        private IAirspace _airspace;

        public TransponderRecieverClient(ITransponderReceiver receiver, ITrackFormatter formatter, ITrackManager manager)
        {
            // This will store the real or the fake transponder data receiver
            _receiver = receiver;

            _airspace = new Airspace(10000, 90000, 10000, 90000, 500, 20000);
            _formatter = formatter;
            _manager = manager;

            // Attach to the event of the real or the fake TDR
            this._receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        public void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // saves and updates for each recieved track/dataset
            foreach (var data in e.TransponderData)
            {
                var track = _formatter.RecieveTrack(data);
                _manager.HandleTrack(track,_airspace);
            }
        }
    }
}
