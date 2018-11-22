using System;
using System.Timers;

namespace ATMPart1
{
    //TODO: find a way of making a fake of the timer, either finding an abstract class of it in System, or make a Interface to wrap it.
    public class EventTimer : IObjectTimer
    {
        private System.Timers.Timer _eTimer;
        
        private IEvent _controlledEvent;

        public event EventHandler<TimerForEventOccuredEventArgs> RaiseTimerOccuredEvent;

        public EventTimer (IEvent evnt, int time)
        {
            _controlledEvent = evnt;

            _eTimer = new System.Timers.Timer();
            _eTimer.Elapsed += OnTimerElapse;
            _eTimer.Interval = time;
            _eTimer.AutoReset = false;
            _eTimer.Enabled = true;
        }

        private void OnTimerElapse(object source, ElapsedEventArgs e)
        {
            _eTimer.Enabled = false;
            OnRaiseTimerOccuredEvent(new TimerForEventOccuredEventArgs(_controlledEvent));
        }

        #region EventHelpers

        protected virtual void OnRaiseTimerOccuredEvent(TimerForEventOccuredEventArgs e)
        {
            EventHandler<TimerForEventOccuredEventArgs> handler = RaiseTimerOccuredEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }

    public class TimerForEventOccuredEventArgs : EventArgs
    {
        public TimerForEventOccuredEventArgs(IEvent evnt)
        {
            Evnt = evnt;
        }

        public IEvent Evnt;
    }

}