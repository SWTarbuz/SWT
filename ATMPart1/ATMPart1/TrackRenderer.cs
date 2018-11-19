using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class TrackRenderer : ITrackRenderer
    {
        private List<ITrack> currTracks;
        private List<ISeperationEvent> currEvents;

        public TrackRenderer(ITrackManager trackManager)
        {
            currTracks = new List<ITrack>();
            currEvents = new List<ISeperationEvent>();

            trackManager.RaiseTracksUpdatedEvent += HandleTrackUpdate;
        }

        void HandleTrackUpdate(object sender, TracksUpdatedEventArgs e)
        {
            UpdateTracks(e.Tracks);
        }

        public void UpdateTracks(List<ITrack> tracks)
        {
            currTracks = tracks;

            Console.Clear();
            WriteOutEvents();
            WriteOutTracks();
        }

        public void UpdateEvents(List<ISeperationEvent> sepEvents)
        {
            currEvents = sepEvents;

            Console.Clear();
            WriteOutEvents();
            WriteOutTracks();
        }

        //TODO: fix me, aka update this to have a proper interface, and then make the rest better with events
        #region Pointless things due to interface

        public void UpdateTracks(IList<ITrack> tracks)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvents(ISeperationEventList events)
        {
            throw new NotImplementedException();
        }

        #endregion

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
    }
}
