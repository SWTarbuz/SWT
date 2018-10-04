﻿using System;
using System.Collections.Generic;
using System.Linq;
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


            if (airspace.IsWithinBounds(Track))
            {
                tracks.Add(Track);
            }


        }
    }
}
