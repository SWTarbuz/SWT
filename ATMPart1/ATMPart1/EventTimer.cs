using System;
using System.Timers;

namespace ATMPart1
{
    //TODO: use this in event list to remove EntryEvents and ExitEvents once they have been active for 5 seconds.
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