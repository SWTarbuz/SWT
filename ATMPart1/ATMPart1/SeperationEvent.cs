using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class SeperationEvent : IEvent
    {
        private ITrack[] _involvedTracks;
        private DateTime _timeOfOccurence;

        #region Properties

        public ITrack[] InvolvedTracks
        {
            get
            {
                return _involvedTracks;
            }
            set { }
        }
        public DateTime TimeOfOccurence
        {
            get { return _timeOfOccurence;} set {}
        }

        #endregion

        

        // setTime Function is not necessary
        public string Print()
        {
            return $"at the time of: {TimeOfOccurence}, the following tracks had a seperation event occur: {InvolvedTracks[0].Tag}, and {InvolvedTracks[1].Tag}";
        }

        // Method for setting time of occurence - Redundant to be deleted?
        public void SetTimeOfOccurence(DateTime time)
        {
            _timeOfOccurence = time;
        }

        public SeperationEvent(ITrack newTrack, ITrack oldTrack)
        {
            if(newTrack.Tag != oldTrack.Tag)
            {
                _involvedTracks = new ITrack[2]{newTrack, oldTrack};
                _timeOfOccurence = newTrack.Timestamp;
            }
        }
    }
}
