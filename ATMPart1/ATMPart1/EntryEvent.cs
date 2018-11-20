using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class EntryEvent : IEvent
    {
        public EventTimer EndTimer { get; } //TODO: consider doing this in interface, of another way

        public ITrack[] InvolvedTracks { get; }
        public DateTime timeOfOccurence { get; }

        public EntryEvent(ITrack track)
        {
            InvolvedTracks = new ITrack[1]{track};
            timeOfOccurence = track.timestamp;

            EndTimer = new EventTimer(this, 5000);
        }

        public string Print()
        {
            return $"at the time of: {timeOfOccurence}, the track: {InvolvedTracks[0].tag}, Entered the Airspace";
        }

        public void setTimeOfOccurence(DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}
