using System.Collections.Generic;
using System.Linq;
using MetroMonitor.DataServices.Exceptions;
using MetroMonitor.DataServices.Extensions;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;
using System.Configuration;

namespace MetroMonitor.DataServices
{
    public class DataAccessService : IDataAccessService
    {
        private readonly IMetroMonitorContext _context;

        public DataAccessService(string connectionString)
            : this(new MetroMonitorContext(connectionString))
        {
        }

        internal DataAccessService(IMetroMonitorContext context)
        {
            _context = context;
        }

        public bool AddNewDevice(DeviceCreate model)
        {
            string deviceName = model.DeviceName;
          //  if (!PingService.IsValidDeviceName(deviceName)) throw new InvalidDeviceNameException(deviceName);
         
            _context.Devices.Add(new Device {Name = model.DeviceName});
            _context.SaveChanges();
            return true;
        }
        
        public void DeleteDevice(DeviceCreate model)
        {
            Device device = _context.Devices.FirstOrDefault(d => d.Id == model.Id);
            if (device == null) return;

            _context.Devices.Remove(device);
            _context.SaveChanges();
        }

        public DeviceList LoadDeviceList()
        {
            IQueryable<Device> devices = from d in _context.Devices select d;
            List<DeviceDetails> deviceCollection = devices.Select(d => new DeviceDetails
                                                                           {
                                                                               Id = d.Id,
                                                                               DeviceName = d.Name
                                                                           }).ToList();

            return new DeviceList
                       {
                           Devices = deviceCollection
                       };
        }

        public DeviceEdit GetDeviceEdit(int id)
        {
            var deviceDetails = (from d in _context.Devices
                                    where d.Id == id
                                    select d).FirstOrDefault();
            if (deviceDetails == null)
                return null;

            var counterDetails = deviceDetails.Counters
                .Where(d => d.Deleted != 1)
                .Select(counter => new DeviceCounterSummary
                                       {
                                           Id = counter.Id,
                                           Description = counter.GetDescription()
                                       })
                .ToList();

            return new DeviceEdit
                       {
                           Id = deviceDetails.Id,
                           DeviceName = deviceDetails.Name,
                           Counters = counterDetails
                       };
        }

        public DeviceDetails GetDeviceDetails(int id)
        {
            var devices = (from d in _context.Devices where d.Id == id select d).FirstOrDefault();

            if (devices != null)
                return new DeviceDetails
                           {
                               Id = devices.Id
                               
                           };
            throw new NonExisitentDeviceException();
        }

        public ResultsData GetResults()
        {
            return new ResultsData {Result = _context.Results.FirstOrDefault()};
        }

        public EditCounter GetCounterEditView(int id)
        {
            throw new System.NotImplementedException();
        }

        public CounterDetails LoadCounterList(CounterCreate model)
        {
            throw new System.NotImplementedException();
        }

        public CounterCreate GetMetricCreateView(int id, CounterCreate type)
        {
            throw new System.NotImplementedException();
        }

        public CounterDelete LoadCounterToDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        public CounterCreate GetMetricDetailsView(CounterCreate model)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteMetric(int id)
        {
            throw new System.NotImplementedException();
        }

        public int AddNewMetric(CounterCreate model)
        {
            var device = _context.Devices.FirstOrDefault(d => d.Id == model.DeviceId);
            //var mapped = Mapper.Map<MetricBase, DeviceCounterBase>(model.Metric);
            //if (mapped is DevicePerformanceCounter)
            //{
            //    _context.PerformanceCounterDefinitions.Attach(((DevicePerformanceCounter) mapped).CounterDefinition);
            //}
            //mapped.Device = device;
            //_context.DeviceCounters.Add(mapped);
            //_context.SaveChanges();

            var metric = (PerformanceCounterMetric)model.Metric;

                var definition =
                    _context.PerformanceCounterDefinitions.FirstOrDefault(d => d.Id == metric.CounterDefinitionId);

                if (metric.InstanceName == null)
                {
                    metric.InstanceName = string.Empty;
                }

                DeviceCounterBase counter = new DevicePerformanceCounter
                                                {
                                                    Device = device,
                                                    LogInterval = model.Metric.LogInterval,
                                                    ReadInterval = model.Metric.ReadInterval,
                                                    MaxThreshold = model.Metric.MaxThreshold,
                                                    MinThreshold = model.Metric.MinThreshold,
                                                    CounterDefinition = definition,
                                                    InstanceName = metric.InstanceName,
                                                    Deleted = 0
                                                };

            _context.DeviceCounters.Add(counter);
            _context.SaveChanges();
            return model.DeviceId;
        }

        public int UpdateMetric(EditCounter model)
        {
            throw new System.NotImplementedException();
        }
    }
}
