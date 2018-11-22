using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class SeperationEvent : IEvent
    {
        //TODO make privates that these get/set to protect from tampering
        public ITrack[] InvolvedTracks { get; set; }
        public DateTime TimeOfOccurence { get; set; }

        // Method for setting time of occurence - Redundant to be deleted?

        // setTime Function is not necessary
        public string Print()
        {
            return $"at the time of: {TimeOfOccurence}, the following tracks had a seperation event occur: {InvolvedTracks[0].Tag}, and {InvolvedTracks[1].Tag}";
        }

        public void SetTimeOfOccurence(DateTime time)
        {
            TimeOfOccurence = time;
        }

        public SeperationEvent(ITrack newTrack, ITrack oldTrack)
        {
            if(newTrack.Tag != oldTrack.Tag)
            {
                InvolvedTracks = new ITrack[2]{newTrack, oldTrack};
                TimeOfOccurence = newTrack.Timestamp;
            }
        }
    }
}
