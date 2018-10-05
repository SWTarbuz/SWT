using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public class TrackFormatter : ITrackFormatter
    {
        /// <summary>
        /// No Security is built into this, thus a piece of illegal data would cause a crash
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ITrack RecieveTrack(string data)
        {
            string[] tokens;
            char[] separators = { ';' };
            string[] formats = {"yyyyMMddHHmmfff"};

            tokens = data.Split(separators, StringSplitOptions.None);
            if (tokens.Length != 5)
            {
                throw new ArgumentOutOfRangeException(nameof(data), tokens.Length, "data contains more or less than 5 strings");
            }


            var time = DateTime.ParseExact(tokens[4], formats[0], CultureInfo.CurrentCulture); //maybe this isn't the right way of parsing it
          

            var track = new Track(tokens[0], float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3]), time);
            return track;
        }
    }
}
