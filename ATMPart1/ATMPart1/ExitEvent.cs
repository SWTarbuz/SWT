using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class ExitEvent : IEvent, ITimedObject
    {
        //TODO make privates that these get/set to protect from tampering
        public IObjectTimer ObjectTimer { get; }
        public ITrack[] InvolvedTracks { get; set; }
        public DateTime TimeOfOccurence { get; set; }

        public ExitEvent(ITrack track)
        {
            InvolvedTracks = new ITrack[1]{track};
            TimeOfOccurence = track.Timestamp;

            ObjectTimer = new EventTimer(this, 5000);
        }

        public string Print()
        {
            return $"at the time of: {TimeOfOccurence}, the track: {InvolvedTracks[0].Tag}, Left the Airspace";
        }

        public void SetTimeOfOccurence(DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}
