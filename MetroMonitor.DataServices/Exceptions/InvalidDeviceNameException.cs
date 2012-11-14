using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices.Exceptions
{
    public class InvalidDeviceNameException: Exception
    {
        public InvalidDeviceNameException(string deviceName)
            : base(string.Format("The specified Device '{0}' does not exist or cannot be contacted", deviceName))
        { }
    }
}
