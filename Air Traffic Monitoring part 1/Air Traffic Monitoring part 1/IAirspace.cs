using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    public interface IAirspace
    {
        int SouthBounds { get; }
        int EastBounds { get; }
        int WestBounds { get; }
        int NorthBounds { get; }
        int LowerAltitudeBound { get; }
        int UpperAltitudeBound { get; }
    }
}
