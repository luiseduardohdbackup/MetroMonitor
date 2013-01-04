using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroMonitor.ViewModels.Results;
using System.Threading.Tasks;


namespace MetroMonitor.ViewModels.Results
{
    public class DeviceStatusResult
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public StatusData.Status Status { get; set; }
    }
    
}
