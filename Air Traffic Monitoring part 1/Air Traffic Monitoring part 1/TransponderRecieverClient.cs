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

        public Reciever(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this._receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this._receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {
                System.Console.WriteLine($"Transponderdata {data}");
            }
        }
    }
}
