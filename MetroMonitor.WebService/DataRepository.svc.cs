using System;
using System.Configuration;
using MetroMonitor.DataServices;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;

namespace MetroMonitor.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataRepository" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataRepository.svc or DataRepository.svc.cs at the Solution Explorer and start debugging.
    public class DataRepository: IDataRepository, IDeviceContracts
    {
        private IDataAccessService _dataAccessService;

        public DataRepository()
        {
            _dataAccessService =
                new DataAccessService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
        }

      

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public ResultsData GetResult()
        {
              return _dataAccessService.GetResults();
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

       public  bool AddDevice(DeviceCreate device) {
           return  _dataAccessService.AddNewDevice(device);
        }


        public  bool DeleteDevice(int deviceId) {
            return _dataAccessService.DeleteDevice(deviceId);
        }

        public bool EditDevice(DeviceEdit device) {

            return _dataAccessService.EditDevice(device);
        }

        public DeviceDataContract DeviceDetails(int DeviceId) {

            return new DeviceDataContract
            {
                deviceDetails = _dataAccessService.GetDeviceDetails(DeviceId)
            };
        }


        public DeviceDataContract LoadDeviceList() {

            return new DeviceDataContract
            {
                devicelist = _dataAccessService.LoadDeviceList()
            };
        }

        #endregion
    }
}
