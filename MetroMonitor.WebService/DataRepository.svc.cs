using System;
using System.Collections.Generic;
using System.Configuration;
using MetroMonitor.DataServices;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.WebService.DataMembers;

namespace MetroMonitor.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataRepository" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataRepository.svc or DataRepository.svc.cs at the Solution Explorer and start debugging.
    public class DataRepository : IDataRepository, IDeviceContracts, ICounterContracts, IGraphContract, IStatisticsContract
    {
        private IDataAccessService _dataAccessService;
        private IDataRepresentationService _dataRepresentationService;
        private IStatisticsProcessingService _statisticsProcessingService;

        public DataRepository()
        {
            _dataAccessService =
                new DataAccessService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);

            _dataRepresentationService =
                new DataRepresentationService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);

            _statisticsProcessingService =
                new StatisticsProcessingService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
  
        }


        public Dictionary<int, string> GetAvailableDevices() {

            return _dataAccessService.GetAvailableDevice();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetResult()
        {
            return new CompositeType { ResultValue = _dataAccessService.GetResults() };
        }

        public CompositeType GetTestData()
        {
            var dataAccessService =
               new DataAccessService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
            var r = dataAccessService.GetResults();
            return new CompositeType
                       {
                           ResultValue = new ResultsData
                                             {
                                                 Result = new Result
                                                              {
                                                                  AverageRead = r.Result.AverageRead,
                                                                  LogDate = r.Result.LogDate
                                                              }
                                             }
                       };
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region Device Operations

        public bool AddDevice(string device)
        {
            return _dataAccessService.AddNewDevice(device);
        }


        public bool DeleteDevice(int deviceId)
        {
            return _dataAccessService.DeleteDevice(deviceId);
        }

        public bool EditDevice(string deviceName, int Id)
        {

            return _dataAccessService.EditDevice(deviceName, Id);
        }

        public DeviceDataContract DeviceDetails(int DeviceId)
        {

            return new DeviceDataContract
            {
                deviceDetails = _dataAccessService.GetDeviceDetails(DeviceId)
            };
        }


        public DeviceDataContract LoadDeviceList()
        {

            return new DeviceDataContract
            {
                devicelist = _dataAccessService.LoadDeviceList()
            };
        }

        #endregion

        #region Counter Operations

        public MetricDetailsData GetMetricDetails(int counterId, int deviceId) {

            var data = _dataAccessService.GetMetricDetails(deviceId, counterId);
            return new MetricDetailsData
            {
                MetricDetails = data
            };
        }

        public Dictionary<int, string> LoadAvailableCounters() {

            return _dataAccessService.GetAvaialbeCounters();
        }

        public CounterDataContract ComboBoxCounterData()
        {

            var counterData =  _dataAccessService.GetCounterList();
            
            var data = new List<CounterComboBox>();
            foreach (var d in counterData) { 
                data.Add(new CounterComboBox{
                    Category = d.Category.Name,
                    Counter = d.Counter.Name,
                    InstanceName = d.InstanceName
                });

                
            }
            return new CounterDataContract
            {
                ComboBoxData = data
            };
        }

        
        public bool AddMetric(CounterCreate counter)
        {

            //looked into this, in msd there is a types screen, bascially you will need to allow the 
            //user to select the counter and instance before and sen send the counter definisionID down
            return _dataAccessService.AddNewMetric(counter);
        }


        public bool DeleteMetric(int id)
        {

            return _dataAccessService.DeleteMetric(id);
        }


        public bool EditMetric(int counterID, int read, int log, int min, int max)
        {

            return _dataAccessService.UpdateMetric(counterID, read, log, min, max);
        }

        public Dictionary<int, string> GetAvailableCountersForDevice(int deviceId) {

        return _dataAccessService.GetCountersForDevice(deviceId);
        

        }

        public CounterDataContract MetricDetails(int deviceId, int counterId)
        {
            return new CounterDataContract
            {
                MetricDetails = _dataAccessService.GetMetricDetails(deviceId, counterId)

            };

        }


        public CounterDataContract LoadMetricList(int deviceId)
        {

            return new CounterDataContract
            {
                MetricDetailsList = _dataAccessService.LoadCounterList(deviceId)
            };
        }

        #endregion

        #region Graph Operations

        public GraphDataContract MetricsOverveiwForDevice(int deviceId){

            var graph = _dataRepresentationService.GetSystemOverviewGraph(deviceId);

            var gdc = new GraphDataContract()
            {
                PlottingData = new Dictionary<GraphCounterDataContract, List<ResultsDataContract>>()
            };

            foreach (var key in graph.XYAxisData) { 
            
                var CounterKey = new GraphCounterDataContract{
                CounterDescription = key.Key.Counter.Description,
                InstanceName = string.Empty
                };

                var ResultsList = new List<ResultsDataContract>();

                foreach(var result in key.Value){
                    var ResultValue = new ResultsDataContract
                    {
                        AverageRead = result.AverageRead,
                        LogDate = result.LogDate,
                        MaximumRead = result.MaximumRead,
                        MinimumRead = result.MinimumRead
                    };
                    ResultsList.Add(ResultValue);

                }
                gdc.PlottingData.Add(CounterKey, ResultsList);
            }

            return gdc;
        }

        public GraphDataContract GetResultsSet(int deviceId) {
            return null; 
          //  return new GraphDataContract { resultSet = _dataRepresentationService.GetResultSet(deviceId) };
        }

        public int TestService() { return 1; }
        
           #endregion

        #region Statistics Operations

        public IEnumerable<DeviceStatusResult> GetStatusesForAllDevices() {

            return _statisticsProcessingService.GetSystemDeviceStatusList();

        }

        public DeviceMetricStatusResult GetCounterSummaryStatus(int id) {

            return _statisticsProcessingService.GetCounterSummaryStatus(id);
        }

        public StatisticsDataContract GetCurrentStatus(int counterId) {

            return new StatisticsDataContract
            {
                status = _statisticsProcessingService.GetCurrentStatus(counterId)
            };
        
        
        }

        #endregion
    }
     

    }



