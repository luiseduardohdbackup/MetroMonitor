using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices.Exceptions
{
    public class NonExisitentDeviceException:Exception
    {
         public NonExisitentDeviceException()
            : base(string.Format("The requested Device Does Not Exist"))
        { }
    }
}
