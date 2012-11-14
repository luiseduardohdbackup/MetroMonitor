namespace MetroMonitor.MonitoringService.Core.Factories
{
    public interface IMetroMonitorContextFactory
    {
        IMetroMonitorContext GetContext();
    }
}