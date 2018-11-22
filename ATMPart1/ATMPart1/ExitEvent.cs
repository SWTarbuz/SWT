using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class ExitEvent : IEvent, ITimedObject
    {
        private ITrack[] _involvedTracks;
        private DateTime _timeOfOccurence;
        private IObjectTimer _objectTimer;

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
            get { return _timeOfOccurence; }
            set { }
        }

        public IObjectTimer ObjectTimer
        {
            get { return _objectTimer; }
            set { }
        }

        #endregion

        public ExitEvent(ITrack track)
        {
            _involvedTracks = new ITrack[1]{track};
            _timeOfOccurence = track.Timestamp;

            _objectTimer = new EventTimer(this, 5000);
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
