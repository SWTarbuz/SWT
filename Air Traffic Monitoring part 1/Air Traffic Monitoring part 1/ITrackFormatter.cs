using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    public interface ITrackFormatter
    {
        ITrack RecieveTrack(string data);
    }
}
