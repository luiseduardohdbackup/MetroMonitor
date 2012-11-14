using MetroMonitor.Entities;
using MetroMonitor.MonitoringService.Core.Counters;

namespace MetroMonitor.MonitoringService.Core.Factories
{
    public class AnalyticsCounterFactory : IAnalyticsCounterFactory
    {
        public AnalyticsCounter CreateCounter(DeviceCounterBase deviceCounter)
        {
            if (deviceCounter is DevicePerformanceCounter)
                return new PerformanceAnalyticsCounter(deviceCounter as DevicePerformanceCounter);
            return null;
        }
    }
}