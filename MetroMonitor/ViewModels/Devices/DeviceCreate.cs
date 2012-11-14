using System.ComponentModel.DataAnnotations;

namespace MetroMonitor.ViewModels.Devices
{
    public class DeviceCreate
    {
        public int Id { get; set; }
        [Display(Name = "System Name")]
        public string DeviceName { get; set; }

        
    }
}
