namespace MetroMonitor.Entities
{
    public class DevicePerformanceCounter : DeviceCounterBase
    {
        public virtual PerformanceCounterDefinition CounterDefinition { get; set; }
        public string Category { get { return CounterDefinition.Category.Name; } }
        public string Name { get { return CounterDefinition.Counter.Name; } }
        public string InstanceName { get; set; }
    }
}