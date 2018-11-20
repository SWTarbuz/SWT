using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class TrackManager : ITrackManager //TODO: Add so that ITrackRenderer is informed of update
    {
        private IList<ITrack> tracks = new List<ITrack>();


        public IList<ITrack> Tracks
        {
            get
            {
               return tracks;
            }
        }

        public event EventHandler<TracksUpdatedEventArgs> RaiseTracksUpdatedEvent;
        public event EventHandler<TracksUpdatedEventArgs> RaiseEntryDetectedEvent;
        public event EventHandler<TracksUpdatedEventArgs> RaiseExitDetectedEvent;

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
                    OnRaiseEntryDetectedEvent(new TracksUpdatedEventArgs(tracks.ToList(), Track));
                }
                
            }
            else // remember to compare tag with tags already in our list, so we can remove old tracks
            {
                foreach (var t in tracks)
                {
                    if (t.tag == Track.tag)
                    {
                        
                        tracks.Remove(t);
                        OnRaiseExitDetectedEvent(new TracksUpdatedEventArgs(tracks.ToList(), Track));
                        return;
                        
                    }
                }
            }

            //TODO: Move so we only call when a change has been made to the list.
            OnRaiseTrackUpdatedEvent(new TracksUpdatedEventArgs(tracks.ToList(), Track));
        }

        #region Helpers

        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
        //seperating so we can override later, and due to us needing to call from multiple places
        protected virtual void OnRaiseTrackUpdatedEvent(TracksUpdatedEventArgs e)
        {
            EventHandler<TracksUpdatedEventArgs> handler = RaiseTracksUpdatedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnRaiseEntryDetectedEvent(TracksUpdatedEventArgs e)
        {
            EventHandler<TracksUpdatedEventArgs> handler = RaiseEntryDetectedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnRaiseExitDetectedEvent(TracksUpdatedEventArgs e)
        {
            EventHandler<TracksUpdatedEventArgs> handler = RaiseExitDetectedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}
