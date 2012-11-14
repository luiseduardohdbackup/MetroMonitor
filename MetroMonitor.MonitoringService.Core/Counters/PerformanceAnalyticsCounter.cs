using System.Collections.Generic;
using System.Linq;
using MetroMonitor.Entities;
using PerformanceCounter = System.Diagnostics.PerformanceCounter;

namespace MetroMonitor.MonitoringService.Core.Counters
{
    public class PerformanceAnalyticsCounter : AnalyticsCounter
    {
        private readonly PerformanceCounter _performanceCounter;
        private readonly List<float> _readings;

        public PerformanceAnalyticsCounter(DevicePerformanceCounter deviceCounter)
            : base(deviceCounter)
        {
            _performanceCounter = new PerformanceCounter(deviceCounter.Category,
                                                         deviceCounter.Name,
                                                         deviceCounter.InstanceName);
            _readings = new List<float>();
        }

        protected override void TakeReading()
        {
            lock (SyncLock)
                _readings.Add(_performanceCounter.NextValue());
        }

        protected override void NotifyResult()
        {
            int intervals;
            float min;
            float max;
            double average;

            lock (SyncLock)
            {
                intervals = _readings.Count;
                min = _readings.Min();
                max = _readings.Max();
                average = _readings.Average();
                _readings.Clear();
            }

            LogResult(intervals, min, max, average);
        }
    }
}