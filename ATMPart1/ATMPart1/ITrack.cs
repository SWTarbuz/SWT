using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ITrack
    {
        string tag { get; }
        float xPos { get; set; }
        float yPos { get; set; }
        float altitude { get; set; }
        float velocity { get; }
        int compassCourse { get; }

        DateTime timestamp { get; }


        void ChangePosition(float x, float y, float alt, DateTime time);
    }
}
