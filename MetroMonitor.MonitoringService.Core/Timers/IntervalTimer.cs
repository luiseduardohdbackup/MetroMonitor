using System.Timers;

namespace MetroMonitor.MonitoringService.Core.Timers
{
    public class IntervalTimer : Timer, IIntervalTimer
    {
        public IntervalTimer() : base() { }
        public IntervalTimer(double interval) : base(interval) { }
    }
}