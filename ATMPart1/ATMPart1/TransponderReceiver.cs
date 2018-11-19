//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace TransponderReceiver
//{
//    public class RawTransponderDataEventArgs : EventArgs
//    {
//        public RawTransponderDataEventArgs(List<string> transponderData)
//        {
//            TransponderData = transponderData;
//        }
//        public List<string> TransponderData { get; set; }
//    }
//    public interface ITransponderReceiver
//    {
//        event EventHandler<RawTransponderDataEventArgs> TransponderDataReady;
//    }

//    public class TransponderReceiverFactory
//    {
//        public static ITransponderReceiver CreateTransponderDataReceiver { get; set; }
//    }
//}
