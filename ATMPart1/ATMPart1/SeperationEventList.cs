using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class SeperationEventList : ISeperationEventList
    {
        private List<ISeperationEvent> _currEvents { get; set; }
        private List<ISeperationEvent> _prevEvents { get; set; }

        public SeperationEventList()
        {
            _currEvents = new List<ISeperationEvent>();
            _prevEvents = new List<ISeperationEvent>();
        }


        public IList<ISeperationEvent> CurrEvents
        {
            get { return _currEvents;}
            set { _currEvents = value.ToList(); }
        }

        public IList<ISeperationEvent> PrevEvents
        {
            get { return _prevEvents; }
        }

        //TODO: 99% sure that he didn't want us to make it update events, except for ending them. Thus further work on this isn't needed.
        public void UpdateCurrEvent(ISeperationEvent sepEvent)
        {
            foreach (var evnt in _currEvents)
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
            _currEvents.Remove(sepEvent);
            _prevEvents.Add(sepEvent);
        }
    }
}
