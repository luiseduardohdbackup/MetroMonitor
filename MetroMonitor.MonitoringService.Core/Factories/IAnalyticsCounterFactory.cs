using MetroMonitor.Entities;
using MetroMonitor.MonitoringService.Core.Counters;

namespace MetroMonitor.MonitoringService.Core.Factories
{
    public interface IAnalyticsCounterFactory
    {
        AnalyticsCounter CreateCounter(DeviceCounterBase deviceCounter);
    }
}