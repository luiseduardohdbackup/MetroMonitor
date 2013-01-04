using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.Entities
{
    public class CounterInstancesForInterface
    {
        public int Id { get; set; }
        public virtual PerformanceCategory Category { get; set; }
        public virtual PerformanceCounter Counter { get; set; }
        public string InstanceName { get; set; }
    }
}
