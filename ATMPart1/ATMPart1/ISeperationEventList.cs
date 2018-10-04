using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Traffic_Monitoring_part_1
{
    public interface ISeperationEventList
    {
        IList<ISeperationEvent> CurrEvents { get; }
        IList<ISeperationEvent> PrevEvents { get; } //maybe this doesn't make sense, if not kill it before it lays eggs

        void UpdateCurrEvent(ISeperationEvent sepEvent, int index);
        void EndEvent(ISeperationEvent sepEvent); //the event to end
    }
}
