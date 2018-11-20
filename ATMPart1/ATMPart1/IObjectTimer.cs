using System;

namespace ATMPart1
{
    public interface IObjectTimer
    {
        event EventHandler<TimerForEventOccuredEventArgs> RaiseTimerOccuredEvent;
    }
}