﻿using System;
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
            TransponderRecieverClient client = new TransponderRecieverClient(reciever, new TrackFormatter(), new TrackManager());

            while (true)
            {
                

            }
        }
    }
}
