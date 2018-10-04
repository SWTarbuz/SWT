using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    class TrackManager : ITrackManager
    {

        private IList<ITrack> tracks;

        public IList<ITrack> Tracks
        {
            get
            {
               return tracks;
            }
        }
        public void HandleTrack(ITrack Track,IAirspace airspace)
        {
            bool check = true;

            if (airspace.IsWithinBounds(Track))
            {
                foreach (var t in tracks)
                {
                    if (t.tag == Track.tag)
                    {
                        t.ChangePosition(Track.xPos,Track.yPos,Track.altitude,Track.timestamp);

                        check = false;
                    }
                    
                }
                if (check)
                {
                    tracks.Add(Track);
                }
                
            }
            else
            {
                foreach (var t in tracks)
                {
                    if (t.tag == Track.tag)
                    {

                    }

                }
            }




        }
    }
}
