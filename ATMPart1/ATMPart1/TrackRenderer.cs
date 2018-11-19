using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    //TODO: Update to either take advantage of the local list, or not have one.
    public class TrackRenderer : ITrackRenderer
    {
        private List<ITrack> currTracks;
        private List<IEvent> currEvents;

        public TrackRenderer(ITrackManager trackManager, IEventList eventList)
        {
            currTracks = new List<ITrack>();
            currEvents = new List<IEvent>();

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
            Console.Clear();
            WriteOutEvents();
            WriteOutTracks();
        }

        private void WriteOutTracks()
        {
            foreach (var track in currTracks)
            {
                System.Console.WriteLine($"track named: {track.tag}, located at x : {track.xPos}, y: {track.yPos}, altitude: {track.altitude}, with air speed velocity at: {track.velocity}, course: {track.compassCourse}, as of: {track.timestamp}");
            }
        }

        private void WriteOutEvents()
        {
            foreach (var evnt in currEvents)
            {
                System.Console.WriteLine($"at the time of: {evnt.timeOfOccurence}, the following tracks had a seperation event occur: {evnt.InvolvedTracks[0].tag}, and {evnt.InvolvedTracks[1].tag}");
            }
        }
        #endregion
    }
}
