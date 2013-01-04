using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Results
{
    public class MetricStatusResult
    {
        public int Id { get; set; }
        public string CounterName { get; set; }
        public IList<MetricTimeFrameResult> TimeFrameResult { get; set; } 
    }
}
