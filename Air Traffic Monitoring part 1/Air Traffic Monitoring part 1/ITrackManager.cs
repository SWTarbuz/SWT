using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    interface ITrackManager
    {
        IList<ITrack> tracks { get; }
        
        void HandleTrack(ITrack track);
    }
}
