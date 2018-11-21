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
        private List<ITrack> _currTracks;
        private List<IEvent> _currEvents;

        private readonly IConsole _console;

        public TrackRenderer(ITrackManager trackManager, IEventList eventList, IConsole console=null)
        {
            _currTracks = new List<ITrack>();
            _currEvents = new List<IEvent>();

            _console = console ?? new WrapThat.SystemBase.Console(); //if null defaults to normal Console

            trackManager.RaiseTracksUpdatedEvent += HandleTrackUpdate;
            eventList.RaiseEventsUpdatedEvent += HandleEventUpdate;
        }

        #region EventHandlers

        void HandleTrackUpdate(object sender, TracksUpdatedEventArgs e)
        {
            _currTracks =  e.Tracks;
            UpdateDisplay();
        }

        void HandleEventUpdate(object sender, RaiseEventsUpdatedEventArgs e)
        {
            _currEvents = e.Events;
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
            foreach (var track in _currTracks)
            {
                _console.WriteLine($"track named: {track.tag}, located at x : {track.xPos}, y: {track.yPos}, altitude: {track.altitude}, with air speed velocity at: {track.velocity}, course: {track.compassCourse}, as of: {track.timestamp}");
            }
        }

        private void WriteOutEvents()
        {
            foreach (var evnt in _currEvents)
            {
                _console.WriteLine(evnt.Print());
            }
        }
        #endregion
    }
}
