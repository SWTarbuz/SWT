using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ITrackManager
    {
        IList<ITrack> Tracks { get; }
        
        void HandleTrack(ITrack track,IAirspace airspace);
    }
}
