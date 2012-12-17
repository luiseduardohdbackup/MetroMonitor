namespace MetroMonitor.ViewModels.Counters
{
    public class CounterCreate
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public CounterBase Metric { get; set; }
        public int CounterDefinitifionId { get; set; }
    }
}