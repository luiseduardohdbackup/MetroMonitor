using MetroMonitor.Entities;

namespace MetroMonitor.ViewModels.Counters
{
    public class EditCounter
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public CounterBase UpdatedCounterDetails { get; set; }
    }
}