using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.Entities;
using System.Data.Objects;
using MetroMonitor.DataServices.Extensions;

namespace MetroMonitor.DataServices
{
    public class StatisticsProcessingService : IStatisticsProcessingService
    {
       private int _upperTrendThreshold = 1;
       private int _lowerTrendThreshold = 1;
       private readonly IMetroMonitorContext _context;

        public StatisticsProcessingService(string connectionString)
            : this(new MetroMonitorContext(connectionString))
        {
        }

        internal StatisticsProcessingService(IMetroMonitorContext context)
        {
            _context = context;
        }

        //public IEnumerable<DeviceMetricStatusResult> GetStatusForAllMetricsForDevice(int deviceId) {
        //    var device = (from d in _context.DeviceCounters where d.Device.Id == deviceId select d).ToList();
        //    var counterStatuses = new List<DeviceMetricStatusResult>();

        //    foreach (var data in device) {
                

        //    }
        
        //}
        public DeviceMetricStatusResult GetCounterSummaryStatus(int id)
        {

            // FRIST CALL STARTS FROM HERE


            //Get Device
            var device = _context.Devices.FirstOrDefault(d => d.Id == id);
            if (device == null)
                return null;



            //Get Counters Associated With Device
            var counters = _context.DeviceCounters
                .Where(c => c.Device.Id == device.Id)
                .Where(c => c.Deleted != 1)
                .Select(c => c).ToList();

            //new up return type
            var deviceCounterStatusResult = new DeviceMetricStatusResult
            {
                Id = id,
                DeviceName = device.Name,
                Statistics = new List<MetricStatusResult>()
            };


            //iterate over each counter
            foreach (var metric in counters)
            {

                var counterStatusResult = new MetricStatusResult
                {
                    Id = metric.Id,
                    CounterName = metric.GetDescription(),
                    TimeFrameResult = new List<MetricTimeFrameResult>()
                };


                var startTimeInterval = 0;
                var endTimeInterval = 0;
                //get status for each time interval
                counterStatusResult.TimeFrameResult.Add(new MetricTimeFrameResult
                {
                    Status =
                        GetCurrentStatus(metric.Id, metric.MinThreshold,
                                         metric.MaxThreshold),
                    Trend =
                        GetCounterTrend(metric.Id, metric.MaxThreshold,
                                        startTimeInterval,
                                        endTimeInterval)

                });

                counterStatusResult.TimeFrameResult.Add(new MetricTimeFrameResult
                {
                    Status =
                        GetCurrentStatusMinus10(metric.Id, metric.MinThreshold,
                                         metric.MaxThreshold),
                    Trend =
                        GetCounterTrend(metric.Id, metric.MaxThreshold,
                                        startTimeInterval,
                                        endTimeInterval)

                });

                counterStatusResult.TimeFrameResult.Add(new MetricTimeFrameResult
                {
                    Status =
                        GetCurrentStatusMinus20(metric.Id, metric.MinThreshold,
                                         metric.MaxThreshold),
                    Trend =
                        GetCounterTrend(metric.Id, metric.MaxThreshold,
                                        startTimeInterval,
                                        endTimeInterval)

                });

                counterStatusResult.TimeFrameResult.Add(new MetricTimeFrameResult
                {
                    Status =
                        GetCurrentStatusMinus30(metric.Id, metric.MinThreshold,
                                         metric.MaxThreshold),
                    Trend =
                        GetCounterTrend(metric.Id, metric.MaxThreshold,
                                        startTimeInterval,
                                        endTimeInterval)

                });


                deviceCounterStatusResult.Statistics.Add(counterStatusResult);
            }

            return deviceCounterStatusResult;

        }
        public StatusData.Trends GetCounterTrend(int counterId, double threshold, int startTime, int endTime)
        {
            var resultSet = (from r in _context.Results
                              .Where(r => r.LogDate >= EntityFunctions.AddMinutes(DateTime.Now, startTime))
                              .Where(r => r.LogDate <= EntityFunctions.AddMinutes(DateTime.Now, endTime))
                              .Where(r => r.DeviceCounter.Id == counterId)
                             select r).ToList();


            var lowerResultSet = (from rs in resultSet
                                  where rs.LogDate >= DateTime.Now.AddMinutes(endTime) &&
                                        rs.LogDate <= DateTime.Now.AddMinutes(startTime / 2)
                                  select rs.AverageRead).ToList();


            var upperResultSet = (from rs in resultSet
                                  where rs.LogDate >= DateTime.Now.AddMinutes(endTime / 2) &&
                                        rs.LogDate <= DateTime.Now.AddMinutes(startTime)
                                  select rs.AverageRead).ToList();

            var processedSetOne = TrendIterator(lowerResultSet, threshold);

            var processedSerTwo = TrendIterator(upperResultSet, threshold);

            var trend = StatusData.Trends.Up;

            if (processedSetOne < processedSerTwo)
            {
                trend = StatusData.Trends.Down;
            }
            else if (processedSetOne <= processedSerTwo + _upperTrendThreshold || processedSetOne >= processedSerTwo - _lowerTrendThreshold)
            {
                trend = StatusData.Trends.Steady;
            }

            return trend;
        }

        private int TrendIterator(IEnumerable<double> resultsSet, double theshold)
        {
            return resultsSet.Count(result => result < theshold);
        }

        public IEnumerable<DeviceStatusResult> GetSystemDeviceStatusList()
        {
          
            var results = new List<DeviceStatusResult>();
            var d = (from dev in _context.Devices where dev.Deleted != 1 select dev).ToList();
            foreach (var server in d)
            {
                var overallStatus = StatusData.Status.Green;
                foreach (var counter in server.Counters.Where(c => c.Deleted != 1))
                {
                   
                     var status = GetCurrentDeviceCounterStatus(counter);
                     if (status > overallStatus)
                         overallStatus = status;
                }

                results.Add(new DeviceStatusResult
                {
                    Id = server.Id,
                    DeviceName = server.Name,
                   
                    Status = overallStatus
                });
            }
            return results;
        }

        private StatusData.Status GetCurrentDeviceCounterStatus(DeviceCounterBase counter)
        {
            var result = _context.Results
                .Where(r => r.DeviceCounter.Id == counter.Id)
                .Where(r => r.LogDate >= EntityFunctions.AddMinutes(DateTime.Now, -5))
                .Select(r => r.AverageRead)
               .FirstOrDefault();

            if (result <= counter.MinThreshold)
            {
                return StatusData.Status.Green;
            }
            return result >= counter.MaxThreshold ? StatusData.Status.Red : StatusData.Status.Yellow;
        }

        public StatusData.Status GetCurrentStatus(int counterId) {

            var status = _context.Results
                                .FirstOrDefault(c => c.DeviceCounter.Id == counterId);


            if (status.AverageRead >= status.DeviceCounter.MaxThreshold) { return StatusData.Status.Red; }

            if (status.AverageRead <= status.DeviceCounter.MinThreshold) { return StatusData.Status.Green; }


            return StatusData.Status.Yellow;
        }

        private StatusData.Status GetStatus(double min, double max, double value, string system)
        {
            if (value >= max)
            {
                
                return StatusData.Status.Red;
            }

            return value <= min ? StatusData.Status.Green : StatusData.Status.Yellow;
        }

        private StatusData.Status GetCurrentStatus(int counterId, double minThreshold, double maxThreshold)
        {
            var stat = _context.Results
                .Where(c => c.DeviceCounter.Id == counterId)
                .Where(t => t.LogDate <= DateTime.Now && t.LogDate >= EntityFunctions.AddSeconds(DateTime.Now, -30))
                .Where(t => t.LogDate <= DateTime.Now)
                .FirstOrDefault();

            if (stat != null)
            {
                return GetStatus(minThreshold, maxThreshold, stat.AverageRead, stat.DeviceCounter.Device.Name);
            }

            return StatusData.Status.Green;
        }

        private StatusData.Status GetCurrentStatusMinus10(int counterId, double minThreshold, double maxThreshold)
        {
            var stat = _context.Results
                .Where(c => c.DeviceCounter.Id == counterId)
                .Where(t => t.LogDate <= DateTime.Now && t.LogDate >= EntityFunctions.AddMinutes(DateTime.Now, -10))
                .FirstOrDefault();

            if (stat != null)
            {
                return GetStatus(minThreshold, maxThreshold, stat.AverageRead, stat.DeviceCounter.Device.Name);
            }

            return StatusData.Status.Green;

        }
        private StatusData.Status GetCurrentStatusMinus20(int counterId, double minThreshold, double maxThreshold)
        {
            var stat = _context.Results
                   .Where(c => c.DeviceCounter.Id == counterId)
                   .Where(t => t.LogDate <= EntityFunctions.AddMinutes(DateTime.Now, -10) && t.LogDate >= EntityFunctions.AddMinutes(DateTime.Now, -20))
                   .FirstOrDefault();

            if (stat != null)
            {
                return GetStatus(minThreshold, maxThreshold, stat.AverageRead, stat.DeviceCounter.Device.Name);
            }
            return StatusData.Status.Green;
        }

        private StatusData.Status GetCurrentStatusMinus30(int counterId, double minThreshold, double maxThreshold)
        {
            var stat = _context.Results
                 .Where(c => c.DeviceCounter.Id == counterId)
                 .Where(t => t.LogDate <= EntityFunctions.AddMinutes(DateTime.Now, -20) && t.LogDate >= EntityFunctions.AddMinutes(DateTime.Now, -30))
                 .FirstOrDefault();

            if (stat != null)
            {
                return GetStatus(minThreshold, maxThreshold, stat.AverageRead, stat.DeviceCounter.Device.Name);
            }
            return StatusData.Status.Green;
        }

    }
    
}
