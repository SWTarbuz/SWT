using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class EventList : IEventList
    {
        private List<IEvent> _currEvents { get; set; }
        private List<IEvent> _prevEvents { get; set; }


        public event EventHandler<RaiseEventsUpdatedEventArgs> RaiseEventsUpdatedEvent;

        public EventList(ITrackManager tm)
        {
            _currEvents = new List<IEvent>();
            _prevEvents = new List<IEvent>();

            tm.RaiseEntryDetectedEvent += HandleRaiseEntryDetectedEvent;
            tm.RaiseExitDetectedEvent += HandleRaiseExitDetectedEvent;
        }

        #region EventHandlers

        private void HandleRaiseEntryDetectedEvent(object sender, TracksUpdatedEventArgs e)
        {
            var evnt = new EntryEvent(e.UpdatedTrack);
            evnt.ObjectTimer.RaiseTimerOccuredEvent += HandleRaiseTimerOccuredEvent;

            _currEvents.Add(evnt);
            OnRaiseEventUpdatedEvent(new RaiseEventsUpdatedEventArgs(_currEvents));
            EventLogger.LogEventToFile(evnt.Print());

        }


        private void HandleRaiseExitDetectedEvent(object sender, TracksUpdatedEventArgs e)
        {
            var evnt = new ExitEvent(e.UpdatedTrack);
            evnt.ObjectTimer.RaiseTimerOccuredEvent += HandleRaiseTimerOccuredEvent;

            _currEvents.Add(evnt);
            OnRaiseEventUpdatedEvent(new RaiseEventsUpdatedEventArgs(_currEvents));
            EventLogger.LogEventToFile(evnt.Print());
        
        }


        private void HandleRaiseTimerOccuredEvent(object source, TimerForEventOccuredEventArgs e)
        {
            _currEvents.Remove(e.Evnt);
            //TODO: consider informing renderer of this event not being relevant anymore.
        }

        #endregion



        public IList<IEvent> CurrEvents
        {
            get { return _currEvents;}
            set { _currEvents = value.ToList(); }
        }

        public IList<IEvent> PrevEvents
        {
            get { return _prevEvents; }
        }
        
        public void UpdateCurrEvent(IEvent sepEvent)
        {
            bool eventExists = false;
            foreach (var evnt in _currEvents)
            {
                if (DoesEventExist(evnt, sepEvent))
                {
                    eventExists = true;
                    break;
                }
            }

            if (eventExists == false)
            {
                _currEvents.Add(sepEvent);
                OnRaiseEventUpdatedEvent(new RaiseEventsUpdatedEventArgs(_currEvents));
                EventLogger.LogEventToFile(sepEvent.Print());
                
            }
        }

        public void EndEvent(IEvent sepEvent)
        {
            _currEvents.Remove(sepEvent);
            _prevEvents.Add(sepEvent);
            OnRaiseEventUpdatedEvent(new RaiseEventsUpdatedEventArgs(_currEvents));
        }

        #region Helpers
        //TODO: Update to take the Entry & Exit Events into consideration.
        private bool DoesEventExist(IEvent event1, IEvent event2)
        {
            bool[] tagsMatch = new bool[2]{false, false};

            // Checks if event1 is a seperation event or not. If not then return false
            if (event1.GetType() != typeof(SeperationEvent)) return false;
  

            // Checks if the seperation event exists
            if (event1.InvolvedTracks[0].Tag == event2.InvolvedTracks[0].Tag ||
                event1.InvolvedTracks[0].Tag == event2.InvolvedTracks[1].Tag) tagsMatch[0] = true;

            if (event1.InvolvedTracks[1].Tag == event2.InvolvedTracks[0].Tag ||
                event1.InvolvedTracks[1].Tag == event2.InvolvedTracks[1].Tag) tagsMatch[1] = true;

            return tagsMatch[0] & tagsMatch[1];
        }

        protected virtual void OnRaiseEventUpdatedEvent(RaiseEventsUpdatedEventArgs e)
        {
            EventHandler<RaiseEventsUpdatedEventArgs> handler = RaiseEventsUpdatedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
