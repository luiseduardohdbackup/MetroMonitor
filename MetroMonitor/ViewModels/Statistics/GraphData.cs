using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.Entities;

namespace MetroMonitor.ViewModels.Statistics
{
    public class GraphData
    {
        public Dictionary<CounterDetails, IList<Result>> XYAxisData { get; set; }
    }
}
