﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class SeperationEventDetector : ISeperationEventDetector
    {
        public ISeperationEventList events { get; private set; }

        public SeperationEventDetector(ISeperationEventList eventList, ITrackManager trackManager) //TODO: Update to use event to renderer instead of this.
        {
            events = eventList;
            trackManager.RaiseTracksUpdatedEvent += HandleTrackUpdate;
        }

        #region eventsAndHandlers

        void HandleTrackUpdate(object sender, TracksUpdatedEventArgs e)
        {
            UpdateEvents(e.UpdatedTrack, e.Tracks);
        }

        #endregion

        // Method for updating events
        public void UpdateEvents(ITrack updatedTrack, IList<ITrack> tracks)
        {
            var cnt = 0;
            foreach (var track in tracks)
            {
                ISeperationEvent detectedEvent = null;

                if (updatedTrack.tag != track.tag) detectedEvent = CompareTracks(updatedTrack, track);

                if (detectedEvent != null)
                {
                    events.UpdateCurrEvent(detectedEvent);
                    cnt++;
                }
            }
        }

        #region Helpers

        // Method for comparing tracks
        private ISeperationEvent CompareTracks(ITrack newTrack, ITrack oldTrack)
        {
            var newXY = Math.Sqrt((Math.Pow(newTrack.xPos, 2)) + (Math.Pow(newTrack.yPos, 2)));
            var oldXY = Math.Sqrt((Math.Pow(oldTrack.xPos, 2)) + (Math.Pow(oldTrack.yPos, 2)));

            if (Math.Abs(newXY - oldXY) < 5000 && Math.Abs(newTrack.altitude - oldTrack.altitude) < 300)
            {
                return new SeperationEvent(newTrack, oldTrack);
            }
            return null;
        }

        #endregion
    }
}
