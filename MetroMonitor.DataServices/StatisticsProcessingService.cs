using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices
{
    public class StatisticsProcessingService : IStatisticsProcessingService
    {
       private readonly IMetroMonitorContext _context;

        public StatisticsProcessingService(string connectionString)
            : this(new MetroMonitorContext(connectionString))
        {
        }

        internal StatisticsProcessingService(IMetroMonitorContext context)
        {
            _context = context;
        }
    }
}
