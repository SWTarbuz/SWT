using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMPart1
{
    public interface ISeperationEventDetector
    {
        IEventList events { get; }

        void UpdateEvents(ITrack updatedTrack, IList<ITrack> tracks);
    }
}
