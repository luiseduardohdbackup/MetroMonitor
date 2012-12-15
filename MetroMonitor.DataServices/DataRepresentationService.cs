using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices
{
    public class DataRepresentationService : IDataRepresentationService
    {
        private readonly IMetroMonitorContext _context;

         public DataRepresentationService(string connectionString)
            : this(new MetroMonitorContext(connectionString))
        {
        }

         internal DataRepresentationService(IMetroMonitorContext context)
        {
            _context = context;
        }
    }
}
