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

        public IEnumerable<CounterInstancesForInterface> GetCounterList()
        {

            return _context.UICounterList.ToList(); 
        }

        public Dictionary<int, string> GetAvaialbeCounters() {

            var c = (from dc in _context.PerformanceCounterDefinitions select dc).ToList();
            var counterList = new Dictionary<int, string>();
            foreach (var r in c) {

                counterList.Add(r.Id, r.Category.Name.ToString() + " " + r.Counter.Name.ToString());
            }
            return counterList;

        }

        public Dictionary<int, string> GetCountersForDevice(int deviceId) { 
        
            var counters = (from c in _context.DeviceCounters where c.Device.Id == deviceId && c.Deleted != 1 select c).ToList();
            var returnData = new Dictionary<int, string>();
            foreach (var data in counters) {
                returnData.Add(data.Id, data.GetDescription().ToString());
            }
            return returnData;
        }

        public bool AddNewDevice(string deviceName)
        {
           
          //  if (!PingService.IsValidDeviceName(deviceName)) throw new InvalidDeviceNameException(deviceName);
         
            _context.Devices.Add(new Device {Name = deviceName, Deleted = 0});
            _context.SaveChanges();
            return true;
        }

        public Dictionary<int, string> GetAvailableDevice() {

            var deviceList = (from d in _context.Devices  where d.Deleted != 1 select d).ToList();

            var resultDictionary = new Dictionary<int, string>();

            foreach (var result in deviceList) {

                resultDictionary.Add(result.Id, result.Name.ToString());
            }

            return resultDictionary;
        }
        
        public bool DeleteDevice(int id)
        {
            Device device = _context.Devices.FirstOrDefault(d => d.Id == id);
            if (device == null) { return false; }

            device.Deleted = 1;
            _context.SaveChanges();
            return true;
        }

        public bool EditDevice(string deviceName, int Id)
        {
            var d = (from device in _context.Devices where device.Id == Id select device).FirstOrDefault();
            if (d != null)
            {
                d.Name = deviceName;
                _context.SaveChanges();
                return true;
            }
            return false;
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

        public IList<CounterDetails> LoadCounterList(int deviceId)
        {
            var metrics = (from c in _context.DeviceCounters where c.Device.Id == deviceId select c).ToList();

            var metricList = new List<CounterDetails>(); 

            foreach (var metric in metrics)
            {
                var counterDetails = new CounterDetails
                {

                    Counter = new CounterBase
                    {
                        Description = metric.GetDescription(),
                        LogInterval = metric.LogInterval,
                        MaxThreshold = (int)metric.MaxThreshold,
                        MinThreshold = (int)metric.MinThreshold,
                        ReadInterval = metric.ReadInterval
                    },

                    Device = new DeviceDetails
                    {
                        DeviceName = metric.Device.Name,
                        Id = metric.Device.Id

                    }
                };

                metricList.Add(counterDetails);

            }
            return metricList;
        }

        public CounterCreate GetMetricCreateView(int id, CounterCreate type)
        {
            throw new System.NotImplementedException();
        }

        public CounterDelete LoadCounterToDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        public CounterDetails GetMetricDetails(int deviceId, int counterId)
        {
            var metric = (from dc in _context.DeviceCounters where dc.Id == counterId && dc.Device.Id == deviceId select dc).FirstOrDefault();

            return new CounterDetails
            {

                Counter = new CounterBase
                {
                    Description = metric.GetDescription(),
                    LogInterval = metric.LogInterval,
                    MaxThreshold = (int)metric.MaxThreshold,
                    MinThreshold = (int)metric.MinThreshold,
                    ReadInterval = metric.ReadInterval
                },

                Device = new DeviceDetails
                {
                    DeviceName = metric.Device.Name,
                    Id = metric.Device.Id

                }
            };
        }

        public bool DeleteMetric(int id)
        {
            var metric = (from d in _context.DeviceCounters where d.Id == id select d).FirstOrDefault();
            if (metric != null)
            {
                metric.Deleted = 1;
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public bool AddNewMetric(CounterCreate model)
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

           // var metric = (PerformanceCounterMetric)model.Metric;

         

                var definition =
                    _context.PerformanceCounterDefinitions.FirstOrDefault(d => d.Id == model.CounterDefinitifionId);

                //if (definition.InstanceName == null)
                //{
                //    metric.InstanceName = string.Empty;
                //}

                DeviceCounterBase counter = new DevicePerformanceCounter
                                                {
                                                    Device = device,
                                                    LogInterval = model.Metric.LogInterval,
                                                    ReadInterval = model.Metric.ReadInterval,
                                                    MaxThreshold = model.Metric.MaxThreshold,
                                                    MinThreshold = model.Metric.MinThreshold,
                                                    CounterDefinition = definition,
                                                    InstanceName = "",
                                                    Deleted = 0
                                                };

            _context.DeviceCounters.Add(counter);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateMetric(int counterID, int read, int log, int min, int max)
        {
            var counterInfo = (from d in _context.DeviceCounters
                               where d.Id == counterID
                               select d).FirstOrDefault();


            if (counterInfo == null)
            {
                return false;
            }
                      

            if (counterInfo is DevicePerformanceCounter)
            {
              //  var UpdateDetails = (PerformanceCounterMetric)model.UpdatedCounterDetails;
                var performanceInfo = (DevicePerformanceCounter)counterInfo;
                                             
                performanceInfo.LogInterval = log;
                performanceInfo.ReadInterval = read;
                performanceInfo.MaxThreshold = max;
                performanceInfo.MinThreshold = min;
                performanceInfo.InstanceName = string.Empty;
                
                _context.SaveChanges();
                return true;

            }
            return false;
        }

      
    }
}
