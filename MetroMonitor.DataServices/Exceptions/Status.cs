using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices.Exceptions
{
    public class StatusData
    {
        public enum Status : byte
        {
            Green,
            Yellow,
            Red
        }
    }
}
