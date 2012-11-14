using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Counters
{
    public class PerformanceCounterMetric : CounterBase
    {
            public IDictionary<int, string> AvailableCounters { get; set; }
            [DisplayName("Counter Type")]
            public int CounterDefinitionId { get; set; }
            [DisplayName("Instance Name")]
            public string InstanceName { get; set; }
     }
}
