using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
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

            tokens = data.Split(separators, 6, StringSplitOptions.None);

            //time is a placeholder, should read this from data
            var track = new Track(tokens[0], float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3]), DateTime.Now);
            return track;
        }
    }
}
