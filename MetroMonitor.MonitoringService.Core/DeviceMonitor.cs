using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Logging;
using MetroMonitor.MonitoringService.Core.Counters;
using MetroMonitor.MonitoringService.Core.Factories;

namespace MetroMonitor.MonitoringService.Core
{
    public class DeviceMonitor
    {
        private static readonly ILog Logger = LogManager.GetLogger<DeviceMonitor>();

        private readonly IMetroMonitorContextFactory _contextFactory;
        private readonly IAnalyticsCounterFactory _counterFactory;
        private readonly string _deviceName;
        private readonly List<AnalyticsCounter> _counters;


        public DeviceMonitor(string connectionString)
            : this(new MetroMonitorContextFactory(connectionString), new AnalyticsCounterFactory())
        {
        }

        public DeviceMonitor(IMetroMonitorContextFactory contextFactory, IAnalyticsCounterFactory counterFactory)
        {
            _contextFactory = contextFactory;
            _counterFactory = counterFactory;
            _deviceName = Environment.MachineName;
            _counters = new List<AnalyticsCounter>();
        }

        public void Initialise()
        {
            Logger.Debug(d => d("Initialising Monitoring on Device {0}", _deviceName));
            using (var context = _contextFactory.GetContext())
            {
                var counters = context.DeviceCounters
                    .Include(x => x.Device)
                    .Where(d => d.Device.Name == _deviceName)
                    .Where(d => d.Deleted != 1)
                    .ToList();
                foreach (var counter in counters
                    .Select(deviceCounter => _counterFactory.CreateCounter(deviceCounter))
                    .Where(counter => counter != null)
                    )
                {
                    counter.ResultGenerated += ResultGenerated;
                    _counters.Add(counter);
                }
            }
            Logger.Info(i => i("Device Monitor Initialised with {0} Counters", _counters.Count));
        }

        public void Start()
        {
            Logger.Debug(d => d("Starting to Monitor"));
            _counters.ForEach(c => c.Start());
        }

        public void Stop()
        {
            Logger.Debug(d => d("Monitoring Stopping"));
            _counters.ForEach(c => c.Stop());
        }

        void ResultGenerated(object sender, ResultEventArgs e)
        {
            Logger.Debug(d => d("Result Generated"));
            using (var context = _contextFactory.GetContext())
            {
                context.DeviceCounters.Attach(e.Result.DeviceCounter);
                context.Results.Add(e.Result);
                context.SaveChanges();


            }
        }
    }
}
