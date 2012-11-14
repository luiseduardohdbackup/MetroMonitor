namespace MetroMonitor.Entities
{
    public class DeviceCounterBase
    {
        public int Id { get; set; }
        public virtual Device Device { get; set; }
        public int ReadInterval { get; set; }
        public int LogInterval { get; set; }
        public double MinThreshold { get; set; }
        public double MaxThreshold { get; set; }
        public int? Deleted { get; set; }
    }
}