using System;

namespace MetroMonitor.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public virtual DeviceCounterBase DeviceCounter { get; set; }
        public DateTime LogDate { get; set; }
        public int Intervals { get; set; }
        public double MinimumRead { get; set; }
        public double MaximumRead { get; set; }
        public double AverageRead { get; set; }
    }
}