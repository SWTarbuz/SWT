using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ISeperationEventList
    {
        IList<IEvent> CurrEvents { get; set; }
        IList<IEvent> PrevEvents { get;} //maybe this doesn't make sense, if not kill it before it lays eggs

        event EventHandler<RaiseEventsUpdatedEventArgs> RaiseEventsUpdatedEvent;

        void UpdateCurrEvent(IEvent sepEvent);
        void EndEvent(IEvent sepEvent); //the event to end
    }

    public class RaiseEventsUpdatedEventArgs : EventArgs
    {
        public RaiseEventsUpdatedEventArgs(List<IEvent> events)
        {
            Events = events;
        }
        public List<IEvent> Events { get; set; }
    }
}
