using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class EnteredAirspaceEvent : IEvent
    {
        public ITrack[] InvolvedTracks { get; }
        public DateTime timeOfOccurence { get; }

        public EnteredAirspaceEvent(ITrack track)
        {
            InvolvedTracks = new ITrack[1]{track};
            timeOfOccurence = track.timestamp;
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
