using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroMonitor.ViewModels.Statistics;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.DataServices.Extensions;
using MetroMonitor.Entities;
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

         public GraphData GetSystemOverviewGraph(int deviceId) {
            
             var AxisData = new GraphData{
                    XYAxisData = new Dictionary<CounterDetails,IList<Result>>()
             };

             var counters = (from c in _context.DeviceCounters where c.Device.Id == deviceId select c).ToList();

             foreach(var counter in counters){

                 var key = new CounterDetails
             {

                 Counter = new CounterBase
                 {
                     Description = counter.GetDescription(),
                     LogInterval = counter.LogInterval,
                     MaxThreshold = (int)counter.MaxThreshold,
                     MinThreshold = (int)counter.MinThreshold,
                     ReadInterval = counter.ReadInterval
                 }
             };

                 var values = (from v in _context.Results where v.DeviceCounter.Id == counter.Id select v).ToList();
                
                 AxisData.XYAxisData.Add(key, values);



             }

             return AxisData;
         }

         public List<Result> GetResultSet(int deviceId) {

             return (from v in _context.Results where v.DeviceCounter.Device.Id == deviceId select v).ToList();
         }
    }
}
