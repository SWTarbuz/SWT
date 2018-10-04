using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            TimeSpan diff = time.Subtract(timestamp);

            if (diff == TimeSpan.Zero) throw new ArgumentException("datetime unchanged", nameof(time));

            CalcVelocityAndCourse(xPos, x, yPos, y, altitude, alt, diff);

            xPos = x;
            yPos = y;
            this.altitude = alt;
            timestamp = time;
        }

        //would be smart to make a setup function that calculates delta's thus reducing amount of arguments to test for in this and calcCourse
        void CalcVelocityAndCourse(float oldX, float newX, float oldY, float newY, float oldAlt, float newAlt, TimeSpan deltaT)
        {
            //setup
            var y = newY - oldY;
            var x = newX - oldX;
            var z = newAlt - oldAlt;

            //calculate velocity in m/s
            var distance = Math.Sqrt(Math.Pow((double)x, 2 ) + Math.Pow((double)y, 2) + Math.Pow((double)z, 2));

            velocity = (float)((double)distance / (double)deltaT.TotalSeconds);

            //use pythagoras to find compass course, as the angle between north (y), and c
            
            int r = (int)Math.Sqrt(Math.Pow((double)x, 2) + Math.Pow((double)y, 2));

            
            //se schaum's outline mathmatical handbook for info om Quadrants, bruger dem omvendt grundet vi gerne vil havde den inverse vinkel.
            if (r == 0) return; //if no movement, keep previous direction
            else if (y >= 0 && x > 0) compassCourse = Math.Abs((int) Math.Round(Math.Asin(x / r) * 360 / (2 * Math.PI))); //Q1
            else if (y < 0 && x >= 0) compassCourse = 90 + Math.Abs((int) Math.Round(Math.Acos(x / r) * 360 / (2 * Math.PI))); //Q4
            else if (y <= 0 && x < 0) compassCourse = 180 + Math.Abs((int) Math.Round(Math.Acos(y / r) * 360 / (2 * Math.PI))); //Q3
            else if (y > 0 && x <= 0) compassCourse = 270 + Math.Abs((int) Math.Round(Math.Asin(y / r) * 360 / (2 * Math.PI))); //Q2
        }
    }
}
