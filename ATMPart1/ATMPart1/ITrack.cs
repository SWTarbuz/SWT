using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ITrack
    {
        string Tag { get; set; }
        float XPos { get; set; }
        float YPos { get; set; }
        float Altitude { get; set; }
        float Velocity { get; }
        int CompassCourse { get; }

        DateTime Timestamp { get; set; }


        void ChangePosition(float x, float y, float alt, DateTime time);
    }
}
