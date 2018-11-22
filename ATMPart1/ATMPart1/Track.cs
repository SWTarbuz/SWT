using System;

namespace ATMPart1
{
    public class Track : ITrack
    {
        private string _tag;
        private float _xPos;
        private float _yPos;
        private float _altitude;
        private float _velocity;
        private int _compassCourse;
        private DateTime _timestamp;

        #region Properties

        public string Tag {
            get { return _tag; } set {}
        }
        public float XPos
        {
            get { return _xPos;} set{}
        }
        public float YPos
        {
            get { return _yPos;} set {}
        }

        public float Altitude
        {
            get { return _altitude;} set {} 

        }
        public float Velocity
        {
            get { return _velocity;} set {}
        }
        public int CompassCourse
        {
            get { return _compassCourse;} set {}
        }
        public DateTime Timestamp
        {
            get { return _timestamp;} set {}
        }

        #endregion
        //TODO: update so that we have privates that these access.

        public Track(string tag, float x, float y, float altitude, DateTime time)
        {
            _tag = tag;
            _xPos = x;
            _yPos = y;
            _altitude = altitude;
            _timestamp = time;
        }

        /// <summary>
        /// changes position and calculates course and velocity
        /// </summary>
        public void ChangePosition(float x, float y, float alt, DateTime time)
        {
            TimeSpan diff = time.Subtract(_timestamp);

            if (diff == TimeSpan.Zero) throw new ArgumentException("datetime unchanged", nameof(time));

            //calcDeltaDistances for use by following Functions
            float[] deltaDistances = CalcDeltaValues(_xPos, x, _yPos, y, _altitude, alt);

            CalcVelocity(deltaDistances[0], deltaDistances[1], deltaDistances[2], diff);
            CalcCourse(deltaDistances[0], deltaDistances[1]);
            
            // Change positions
            _xPos = x;
            _yPos = y;
            _altitude = alt;
            _timestamp = time;
        }

        /// <summary>
        /// Calculates the delta's on x, y and altitude (z)
        /// </summary>
        float[] CalcDeltaValues(float oldX, float newX, float oldY, float newY, float oldAlt, float newAlt)
        {
            return new float[3]{(newX-oldX), (newY-oldY), (newAlt-oldAlt)};
        }

        // Would be smart to make a setup function that calculates delta's thus reducing amount of arguments to test for in this and calcCourse
        // Calculate velocity in m/s
        void CalcVelocity(float dX, float dY, float dZ, TimeSpan deltaT)
        {
            var distance = Math.Sqrt(Math.Pow((double)dX, 2 ) + Math.Pow((double)dY, 2) + Math.Pow((double)dZ, 2));

            _velocity = (float)((double)distance / (double)deltaT.TotalSeconds);
        }


        // Calc course 0 = north, 90 = east, 180 = south 270 = west, and so forth
        void CalcCourse(float dX, float dY)
        {
            int r = (int)Math.Sqrt(Math.Pow((double)dX, 2) + Math.Pow((double)dY, 2));

            // Se schaum's outline mathmatical handbook for info om Quadrants, bruger dem omvendt grundet vi gerne vil havde den inverse vinkel.
            if (r == 0) return; //if no movement, keep previous direction
            else if (dY >= 0 && dX > 0) _compassCourse = Math.Abs((int)Math.Round(Math.Asin(dX / r) * 360 / (2 * Math.PI))); //Q1
            else if (dY < 0 && dX >= 0) _compassCourse = 90 + Math.Abs((int)Math.Round(Math.Acos(dX / r) * 360 / (2 * Math.PI))); //Q4
            else if (dY <= 0 && dX < 0) _compassCourse = 90 + Math.Abs((int)Math.Round(Math.Acos(dX / r) * 360 / (2 * Math.PI))); //Q3 (NOT SURE WHY BUT, THE MATH ADDS UP WITH +90, INSTEAD OF +180)
            else if (dY > 0 && dX <= 0) _compassCourse = 270 + Math.Abs((int)Math.Round(Math.Asin(dY / r) * 360 / (2 * Math.PI))); //Q2

            if (_compassCourse == 360) _compassCourse = 0; // Fix to ensure values between 0 and 359
        }
    }
}
