using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface IEvent
    {
        ITrack[] InvolvedTracks { get; } //make it to 2 tracks for now
        DateTime timeOfOccurence { get; }

        string Print();
        void setTimeOfOccurence(DateTime time);
    }
}
