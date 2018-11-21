using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrapThat.SystemBase;
using Console = System.Console;

namespace ATMPart1
{
    //TODO: Update to either take advantage of the local list, or not have one.
    public class TrackRenderer : ITrackRenderer
    {
        private List<ITrack> currTracks;
        private List<IEvent> currEvents;

        private readonly IConsole _console;

        public TrackRenderer(ITrackManager trackManager, IEventList eventList, IConsole console=null)
        {
            currTracks = new List<ITrack>();
            currEvents = new List<IEvent>();

            _console = console ?? new WrapThat.SystemBase.Console(); //if null defaults to normal Console

            trackManager.RaiseTracksUpdatedEvent += HandleTrackUpdate;
            eventList.RaiseEventsUpdatedEvent += HandleEventUpdate;
        }

        #region EventHandlers

        void HandleTrackUpdate(object sender, TracksUpdatedEventArgs e)
        {
            currTracks =  e.Tracks;
            UpdateDisplay();
        }

        void HandleEventUpdate(object sender, RaiseEventsUpdatedEventArgs e)
        {
            currEvents = e.Events;
            UpdateDisplay();
        }

        #endregion

        #region Helpers

        private void UpdateDisplay()
        {
            _console.Clear();
            WriteOutEvents();
            WriteOutTracks();
        }

        private void WriteOutTracks()
        {
            foreach (var track in currTracks)
            {
                _console.WriteLine($"track named: {track.tag}, located at x : {track.xPos}, y: {track.yPos}, altitude: {track.altitude}, with air speed velocity at: {track.velocity}, course: {track.compassCourse}, as of: {track.timestamp}");
            }
        }

        private void WriteOutEvents()
        {
            foreach (var evnt in currEvents)
            {
                _console.WriteLine(evnt.Print());
            }
        }
        #endregion
    }
}
