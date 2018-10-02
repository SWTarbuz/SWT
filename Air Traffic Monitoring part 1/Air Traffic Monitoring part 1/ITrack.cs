using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    interface ITrack
    {
        string tag { get; }
        float xPos { get; }
        float yPos { get; }
        float altitude { get; }
        float velocity { get; }
        int compassCourse { get; }
    }
}
