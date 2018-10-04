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

        public void ChangePosition(float x, float y, float alt, DateTime time)
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

            //setup
            var y = newY - oldY;
            var x = newX - oldX;
            var z = newAlt - oldAlt;

            //calculate velocity in m/s
            var distance = Math.Sqrt(Math.Pow((double)x, 2 ) + Math.Pow((double)y, 2) + Math.Pow((double)z, 2));

            velocity = (float)((double)distance / (double)diff.TotalSeconds);

            //use pythagoras to find compass course, as the angle between north (y), and c
            
            int c = (int)Math.Sqrt(Math.Pow((double)x, 2) + Math.Pow((double)y, 2));

            if (y > 0 && x > 0) compassCourse = (int)Math.Acos(y / c); //0 to 90 degrees (N to E) y and x greater than 0
            else if (y < 0 && x > 0) compassCourse = (int)Math.Acos(y / c) + 90; //90 to 180 degrees (E to S) y less than 0 and x greater than 0
            else if (y < 0 && x < 0) compassCourse = (int)Math.Acos(y / c) + 180; //180 to 270 degrees (S to W) y and x less than 0
            else if (y > 0 && x < 0) compassCourse = (int)Math.Acos(y / c) + 270; //270 to 360 degrees (W to N) y greater than 0 and x less than 0
        }
    }
}
