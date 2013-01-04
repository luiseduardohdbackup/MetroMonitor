using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Results
{
    public class MetricTimeFrameResult
    {
        public StatusData.Status Status { get; set; }
        public StatusData.Trends Trend { get; set; }
    }
}
