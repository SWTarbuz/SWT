using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class Track : ITrack
    {
        public string tag { get; private set; }
        public float xPos { get; private set; }
        public float yPos { get; private set; }
        public float altitude { get; private set; }
        public float velocity { get; private set; }
        public int compassCourse { get; private set; }
        public DateTime timestamp { get; private set; }

        public Track(string tag, float x, float y, float altitude, DateTime time)
        {
            this.tag = tag;
            xPos = x;
            yPos = y;
            this.altitude = altitude;
            timestamp = time;

        }

        void ChangePosition(float x, float y, float alt, DateTime time)
        {
            CalcVelocityAndCourse(xPos, x, yPos, y, altitude, alt, timestamp, time);

            xPos = x;
            yPos = y;
            this.altitude = alt;
            timestamp = time;
        }

        void CalcVelocityAndCourse(float oldX, float newX, float oldY, float newY, float oldAlt, float newAlt, DateTime oldTime, DateTime newTime)
        {
            TimeSpan diff = newTime.Subtract(oldTime);

            //var distance = Math.Sqrt(Math.Pow((newX, oldX), 2) + );

        }
    }
}
