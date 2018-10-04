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
        float xPos { get; }
        float yPos { get; }
        float altitude { get; }
        float velocity { get; }
        int compassCourse { get; }

        DateTime timestamp { get; }
    }
}
