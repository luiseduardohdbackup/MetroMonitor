using System;
using MetroMonitor.Entities;

namespace MetroMonitor.MonitoringService.Core
{
    public class ResultEventArgs : EventArgs
    {
        public ResultEventArgs(Result result)
        {
            Result = result;
        }

        public Result Result { get; private set; }
    }
}