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
        public bool IsWithinBounds(ITrack track)
        {
            if (track.xPos >= WestBounds && track.xPos <= EastBounds && track.yPos >= SouthBounds &&
                track.yPos <= NorthBounds && track.altitude >= LowerAltitudeBound &&
                track.altitude <= UpperAltitudeBound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Airspace(int SouthBounds_, int EastBounds_, int WestBounds_, int NorthBounds_, int LowerAltitudeBound_, int UpperAltitudeBound_)
        {
            SouthBounds = SouthBounds_;
            EastBounds = EastBounds_;
            WestBounds = WestBounds_;
            NorthBounds = NorthBounds_;
            LowerAltitudeBound = LowerAltitudeBound_;
            UpperAltitudeBound = UpperAltitudeBound_;
        }

    }
}
