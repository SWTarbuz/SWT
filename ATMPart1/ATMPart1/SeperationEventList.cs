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
            bool eventExists = false;
            foreach (var evnt in _currEvents)
            {
                if (DoesEventExist(evnt, sepEvent))
                {
                    //TODO: implement update renderer when event is added.
                    eventExists = true;
                    break;
                }
            }

            if (eventExists == false) _currEvents.Add(sepEvent);
        }

        public void EndEvent(ISeperationEvent sepEvent)
        {
            _currEvents.Remove(sepEvent);
            _prevEvents.Add(sepEvent);
        }

        #region Helpers

        private bool DoesEventExist(ISeperationEvent event1, ISeperationEvent event2)
        {
            bool[] tagsMatch = new bool[2]{false, false};

            if (event1.InvolvedTracks[0].tag == event2.InvolvedTracks[0].tag ||
                event1.InvolvedTracks[0].tag == event2.InvolvedTracks[1].tag) tagsMatch[0] = true;

            if (event1.InvolvedTracks[1].tag == event2.InvolvedTracks[0].tag ||
                event1.InvolvedTracks[1].tag == event2.InvolvedTracks[1].tag) tagsMatch[1] = true;

            return tagsMatch[0] & tagsMatch[1];
            //if (tagsMatch[0] == true && tagsMatch[1] == true) return true;
            //return false;
        }

        #endregion
    }
}
