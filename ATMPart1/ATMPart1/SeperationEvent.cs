using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class SeperationEvent : ISeperationEvent
    {
        public ITrack[] InvolvedTracks { get; private set; }
        public DateTime timeOfOccurence { get; private set; }
        public void setTimeOfOccurence(DateTime time)
        {
            timeOfOccurence = time;
        }

        public SeperationEvent(ITrack newTrack, ITrack oldTrack)
        {
            InvolvedTracks = new ITrack[2]{newTrack, oldTrack};
            timeOfOccurence = newTrack.timestamp;
        }
    }
}
