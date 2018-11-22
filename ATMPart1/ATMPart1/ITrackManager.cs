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
        event EventHandler<TracksUpdatedEventArgs> RaiseTracksUpdatedEvent;
        event EventHandler<TracksUpdatedEventArgs> RaiseEntryDetectedEvent;
        event EventHandler<TracksUpdatedEventArgs> RaiseExitDetectedEvent;
        void HandleTrack(ITrack track,IAirspace airspace);
    }


    public class TracksUpdatedEventArgs : EventArgs
    {
        public TracksUpdatedEventArgs(List<ITrack> tracks, ITrack updatedTrack)
        {
            Tracks = tracks;
            UpdatedTrack = updatedTrack;
        }

        public List<ITrack> Tracks { get; set; }
        public ITrack UpdatedTrack { get; set; }
    }
}
