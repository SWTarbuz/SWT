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
        public DateTime timeOfOccurence { get; set; }

        // Method for setting time of occurence - Redundant to be deleted?

        // setTime Function is not necessary
        public string Print()
        {
            return $"at the time of: {timeOfOccurence}, the following tracks had a seperation event occur: {InvolvedTracks[0].tag}, and {InvolvedTracks[1].tag}";
        }

        public void setTimeOfOccurence(DateTime time)
        {
            timeOfOccurence = time;
        }

        public SeperationEvent(ITrack newTrack, ITrack oldTrack)
        {
            //TODO: add something to ensure that the tracks can't be the same / have the same tag.
            //Think it is fixed by encapsulating with an If statement
            if(newTrack.tag != oldTrack.tag)
            {
                InvolvedTracks = new ITrack[2]{newTrack, oldTrack};
                timeOfOccurence = newTrack.timestamp;
            }
        }
    }
}
