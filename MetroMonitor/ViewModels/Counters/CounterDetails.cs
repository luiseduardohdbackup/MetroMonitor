using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Devices;

namespace MetroMonitor.ViewModels.Counters
{
    public class CounterDetails
    {
        public DeviceDetails Device { get; set; }
        public CounterBase Counter{ get; set; }
    }
}