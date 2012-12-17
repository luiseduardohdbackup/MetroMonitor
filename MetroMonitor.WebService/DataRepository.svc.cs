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

        public bool AddDevice(DeviceCreate device)
        {
            return _dataAccessService.AddNewDevice(device);
        }


        public bool DeleteDevice(int deviceId)
        {
            return _dataAccessService.DeleteDevice(deviceId);
        }

        public bool EditDevice(DeviceEdit device)
        {

            return _dataAccessService.EditDevice(device);
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


        public bool EditMetric(EditCounter counter)
        {

            return _dataAccessService.UpdateMetric(counter);
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
        
            return new GraphDataContract{            
                PlottingData = _dataRepresentationService.GetSystemOverviewGraph(deviceId)
            };
        }

        public GraphDataContract GetResultsSet(int deviceId) {

            return new GraphDataContract { resultSet = _dataRepresentationService.GetResultSet(deviceId) };
        }

        public int TestService() { return 1; }
        
           #endregion

        #region Statistics Operations

        public StatisticsDataContract GetCurrentStatus(int counterId) {

            return new StatisticsDataContract
            {
                status = _statisticsProcessingService.GetCurrentStatus(counterId)
            };
        
        
        }

        #endregion
    }
     

    }



