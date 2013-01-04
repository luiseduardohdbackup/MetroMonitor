using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Results
{
    public class DeviceMetricStatusResult
    {
        public string DeviceName { get; set; }
        public int Id { get; set; }
        public List<MetricStatusResult> Statistics { get; set; }
    }
}
