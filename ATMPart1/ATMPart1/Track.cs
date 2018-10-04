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

        /// <summary>
        /// changes position and calculates course and velocity
        /// </summary>
        public void ChangePosition(float x, float y, float alt, DateTime time)
        {
            TimeSpan diff = time.Subtract(timestamp);

            if (diff == TimeSpan.Zero) throw new ArgumentException("datetime unchanged", nameof(time));

            //calcDeltaDistances for use by following Functions
            float[] deltaDistances = CalcDeltaValues(xPos, x, yPos, y, altitude, alt);

            CalcVelocity(deltaDistances[0], deltaDistances[1], deltaDistances[2], diff);
            CalcCourse(deltaDistances[0], deltaDistances[1]);
            
            //change positions
            xPos = x;
            yPos = y;
            this.altitude = alt;
            timestamp = time;
        }

        /// <summary>
        /// Calculates the delta's on x, y and altitude (z)
        /// </summary>
        float[] CalcDeltaValues(float oldX, float newX, float oldY, float newY, float oldAlt, float newAlt)
        {
            return new float[3]{(newX-oldX), (newY-oldY), (newAlt-oldAlt)};
        }

        //would be smart to make a setup function that calculates delta's thus reducing amount of arguments to test for in this and calcCourse
        //calculate velocity in m/s
        void CalcVelocity(float dX, float dY, float dZ, TimeSpan deltaT)
        {
            var distance = Math.Sqrt(Math.Pow((double)dX, 2 ) + Math.Pow((double)dY, 2) + Math.Pow((double)dZ, 2));

            velocity = (float)((double)distance / (double)deltaT.TotalSeconds);
        }

        //calc course 0 = north, 90 = east, 180 = south 270 = west, and so forth
        void CalcCourse(float dX, float dY)
        {
            int r = (int)Math.Sqrt(Math.Pow((double)dX, 2) + Math.Pow((double)dY, 2));

            //se schaum's outline mathmatical handbook for info om Quadrants, bruger dem omvendt grundet vi gerne vil havde den inverse vinkel.
            if (r == 0) return; //if no movement, keep previous direction
            else if (dY >= 0 && dX > 0) compassCourse = Math.Abs((int)Math.Round(Math.Asin(dX / r) * 360 / (2 * Math.PI))); //Q1
            else if (dY < 0 && dX >= 0) compassCourse = 90 + Math.Abs((int)Math.Round(Math.Acos(dX / r) * 360 / (2 * Math.PI))); //Q4
            else if (dY <= 0 && dX < 0) compassCourse = 180 + Math.Abs((int)Math.Round(Math.Acos(dY / r) * 360 / (2 * Math.PI))); //Q3
            else if (dY > 0 && dX <= 0) compassCourse = 270 + Math.Abs((int)Math.Round(Math.Asin(dY / r) * 360 / (2 * Math.PI))); //Q2

            if (compassCourse == 360) compassCourse = 0; //fix to ensure values between 0 and 359
        }
    }
}
