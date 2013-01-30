using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.WebService.DataMembers;

namespace MetroMonitor.WebService
{
    [ServiceContract]
    public interface IDeviceContracts
    {
        [OperationContract]
        bool AddDevice(string device);

        [OperationContract]
        bool DeleteDevice(int deviceId);
       
        [OperationContract]
        bool EditDevice(string deviceName, int Id);
        
        [OperationContract]
        DeviceDataContract DeviceDetails(int DeviceId);

        [OperationContract]
        DeviceDataContract LoadDeviceList();
    }

    
}