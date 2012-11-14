namespace MetroMonitor.Entities
{
    public class PerformanceCounterDefinition
    {
        public int Id { get; set; }
        public virtual PerformanceCategory Category { get; set; }
        public virtual PerformanceCounter Counter { get; set; }
    }
}