using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class EntryEvent : IEvent, ITimedObject
    {
        //TODO make privates that these get/set to protect from tampering
        public IObjectTimer ObjectTimer { get; }
        public ITrack[] InvolvedTracks { get; set; }
        public DateTime timeOfOccurence { get; set; }

        public EntryEvent(ITrack track)
        {
            InvolvedTracks = new ITrack[1]{track};
            timeOfOccurence = track.timestamp;

            ObjectTimer = new EventTimer(this, 5000);
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
