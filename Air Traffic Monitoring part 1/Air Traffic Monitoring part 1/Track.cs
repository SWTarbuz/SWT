using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    class Track : ITrack
    {
        public string tag { get; private set; }
        public float xPos { get; private set; }
        public float yPos { get; private set; }
        public float altitude { get; private set; }
        public float velocity { get; private set; }
        public int compassCourse { get; private set; }

        public Track(string tag, float x, float y, float altitude, float velocity, int course)
        {
            this.tag = tag;
            xPos = x;
            yPos = y;
            this.altitude = altitude;
            this.velocity = velocity;
            this.compassCourse = course;
        }

        void ChangePosition(float x, float y, float altitude, float velocity, int course)
        {
            xPos = x;
            yPos = y;
            this.altitude = altitude;
            this.velocity = velocity;
            this.compassCourse = course;
        }
    }
}
