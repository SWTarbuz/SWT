﻿using System;
using System.Timers;

namespace ATMPart1
{
    //TODO: use this in event list to remove EntryEvents and ExitEvents once they have been active for 5 seconds.
    public class EventTimer
    {
        private System.Timers.Timer _eTimer;

        private IEvent _controlledEvent;

        private EventHandler<TimerForEventOccuredEventArgs> RaiseTimerOccuredEvent;

        public EventTimer (IEvent evnt, int time)
        {
            _controlledEvent = evnt;

            _eTimer = new System.Timers.Timer();
            _eTimer.Elapsed += OnTimerElapse;
            _eTimer.Interval = time;
            _eTimer.Enabled = true;
        }

        private void OnTimerElapse(object source, ElapsedEventArgs e)
        {
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
            _event = evnt;
        }

        public IEvent _event;
    }

}