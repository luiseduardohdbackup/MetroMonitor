using System.ComponentModel.DataAnnotations;

namespace MetroMonitor.ViewModels.Devices
{
    public class DeviceDetails
    {
        public int Id { get; set; }
       [Display(Name = "System Name")]
        public string DeviceName { get; set; }
       [Display(Name = "Machine Name")]
       public string Description { get; set; }
   }
}
