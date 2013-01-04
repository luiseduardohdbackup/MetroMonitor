using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Results
{
    public static class StatusData
    {
        public enum Status : byte
        {
            Green,
            Yellow,
            Red
        }

        public enum Trends : byte
        {
            Down,
            Steady,
            Up
        }
    }
}
