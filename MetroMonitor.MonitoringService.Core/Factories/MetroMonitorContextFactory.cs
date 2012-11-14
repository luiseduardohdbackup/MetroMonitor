namespace MetroMonitor.MonitoringService.Core.Factories
{
    public class MetroMonitorContextFactory : IMetroMonitorContextFactory
    {
        private readonly string _connectionString;

        public MetroMonitorContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IMetroMonitorContext GetContext()
        {
            return new MetroMonitorContext(_connectionString);
        }
    }
}