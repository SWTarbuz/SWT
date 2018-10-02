using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    interface ISeperationEvent
    {
        ITrack[] InvolvedTracks { get; } //make it to 2 tracks for now
        DateTime timeOfOccurence { get; }

        void setTimeOfOccurence(DateTime time);
    }
}
