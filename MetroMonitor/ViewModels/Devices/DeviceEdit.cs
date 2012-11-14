using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetroMonitor.ViewModels.Devices
{
    public class DeviceEdit
    {
        public int Id { get; set; }
        [Display(Name = "Server Name")]
        [StringLength(50, MinimumLength = 5)]
        [Required]
        public string DeviceName { get; set; } 
        public IList<DeviceCounterSummary> Counters { get; set; }
       
    }
}
