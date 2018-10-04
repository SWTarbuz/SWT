using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    class TrackManager : ITrackManager
    {
        public IList<ITrack> tracks => throw new NotImplementedException();

        public void HandleTrack(ITrack Track,IAirspace airspace)
        {
            airspace.IsWithinBounds(Track);

        }
    }
}
