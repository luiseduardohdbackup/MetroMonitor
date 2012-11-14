using System.ComponentModel;

namespace MetroMonitor.MonitoringService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public const string SystemServiceName = "MetroMonitorService";
        public ProjectInstaller()
        {
            InitializeComponent();
            serviceInstaller.ServiceName = SystemServiceName;
        }
    }
}
