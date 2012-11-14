using System.Configuration;
using System.ServiceProcess;
using Common.Logging;
using MetroMonitor.MonitoringService.Core;

namespace MetroMonitor.MonitoringService
{
    partial class MonitorService : ServiceBase
    {
        private static readonly ILog Logger = LogManager.GetLogger<MonitorService>();
        private DeviceMonitor _deviceMonitor;

        public MonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info(i => i("Initialising Device Monitor"));
            _deviceMonitor =
                new DeviceMonitor(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
            _deviceMonitor.Initialise();
            _deviceMonitor.Start();
            Logger.Info(i => i("Service Successfully Started"));
        }

        protected override void OnStop()
        {
            Logger.Info(i => i("Stopping Device Monitor"));
            _deviceMonitor.Stop();
            Logger.Info(i => i("Service Successfully Stopped"));
        }
    }
}
