using System.Collections.Generic;

namespace MetroMonitor.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DeviceCounterBase> Counters { get; set; } 
    }
}