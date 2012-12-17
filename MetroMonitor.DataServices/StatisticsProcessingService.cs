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

        public Status GetCurrentStatus(int counterId) {

            var status = _context.Results
                .FirstOrDefault(c => c.DeviceCounter.Id == counterId);
             

            if (status.AverageRead >= status.DeviceCounter.MaxThreshold) return Status.Red;

            return Status.Green;
        }

    }

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
