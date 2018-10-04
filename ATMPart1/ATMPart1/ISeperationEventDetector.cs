using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ISeperationEventDetector
    {
        void UpdateEvents(ITrack updatedTrack);
    }
}
