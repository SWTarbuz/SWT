using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    class SeperationEventList : ISeperationEventList
    {
        private List<ISeperationEvent> currEvents { get; set; }
        private List<ISeperationEvent> prevEvents { get; set; }

        public IList<ISeperationEvent> CurrEvents
        {
            get { return currEvents;}
            set { currEvents = value.ToList(); }
        }

        public IList<ISeperationEvent> PrevEvents
        {
            get { return prevEvents; }
        }

        //TODO: 99% sure that he didn't want us to make it update events, except for ending them. Thus further work on this isn't needed.
        public void UpdateCurrEvent(ISeperationEvent sepEvent)
        {
            foreach (var evnt in currEvents)
            {
                if (evnt.InvolvedTracks == sepEvent.InvolvedTracks)
                {
                    //TODO: implement update
                    break;
                }
            }
        }

        public void EndEvent(ISeperationEvent sepEvent)
        {
            currEvents.Remove(sepEvent);
            prevEvents.Add(sepEvent);
        }
    }
}
