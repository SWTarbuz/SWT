using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMPart1;

namespace ATMPart1
{
    public class Airspace : IAirspace
    {
        public int SouthBounds { get; private set; }
        public int EastBounds { get; private set; }
        public int WestBounds { get; private set; }
        public int NorthBounds { get; private set; }
        public int LowerAltitudeBound { get; private set; }
        public int UpperAltitudeBound { get; private set; }

        // Checks if within altitude upper and lower bounds
        public bool IsWithinBounds(ITrack track)
        {
            if (track.XPos >= WestBounds && track.XPos <= EastBounds && track.YPos >= SouthBounds &&
                track.YPos <= NorthBounds && track.Altitude >= LowerAltitudeBound &&
                track.Altitude <= UpperAltitudeBound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Constructor for Airspace
        public Airspace(int southBounds, int eastBounds, int westBounds, int northBounds, int lowerAltitudeBound, int upperAltitudeBound)
        {
            SouthBounds = southBounds;
            EastBounds = eastBounds;
            WestBounds = westBounds;
            NorthBounds = northBounds;
            LowerAltitudeBound = lowerAltitudeBound;
            UpperAltitudeBound = upperAltitudeBound;
        }

    }
}
