using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace Air_Traffic_Monitoring_part_1
{
    class TransponderRecieverClient
    {
        private ITransponderReceiver _receiver;
        private ITrackFormatter _formatter;
        private ITrackManager _manager;

        public TransponderRecieverClient(ITransponderReceiver receiver, ITrackFormatter formatter, ITrackManager manager)
        {
            // This will store the real or the fake transponder data receiver
            this._receiver = receiver;

            _formatter = formatter;
            _manager = manager;

            // Attach to the event of the real or the fake TDR
            this._receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // saves and updates for each recieved track/dataset
            foreach (var data in e.TransponderData)
            {
                var track = _formatter.RecieveTrack(data);
                _manager.HandleTrack(track);
            }
        }
    }
}
