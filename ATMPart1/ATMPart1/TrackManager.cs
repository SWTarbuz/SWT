using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class TrackManager : ITrackManager
    {

        private IList<ITrack> tracks = new List<ITrack>();

        public IList<ITrack> Tracks
        {
            get
            {
               return tracks;
            }
        }
        public void HandleTrack(ITrack Track,IAirspace airspace)
        {
            bool check = true; //bool to make a check after list loop

            if (airspace.IsWithinBounds(Track)) // check if within bounds
            {
                foreach (var t in tracks) //loop through list
                {
                    if (t.tag == Track.tag) //compare new track tag to tracks already known
                    {
                        t.ChangePosition(Track.xPos,Track.yPos,Track.altitude,Track.timestamp); //known tag, change position

                        check = false;
                    }
                    
                }
                if (check)
                {
                    tracks.Add(Track); //new tag, just add it
                }
                
            }
            else // remember to compare tag with tags already in our list, so we can remove old tracks
            {
                foreach (var t in tracks)
                {
                    if (t.tag == Track.tag)
                    {
                        
                        tracks.Remove(t);
                        return;
                        
                    }
                }
            }
        }
    }
}
