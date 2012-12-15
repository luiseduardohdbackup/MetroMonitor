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
namespace MetroMonitor.WebService
{
    [ServiceContract]
    public interface IDeviceContracts
    {
        [OperationContract]
        bool AddDevice(DeviceCreate device);

        [OperationContract]
        bool DeleteDevice(int deviceId);
       
        [OperationContract]
        bool EditDevice(DeviceEdit device);
        
        [OperationContract]
        DeviceDataContract DeviceDetails(int DeviceId);

        [OperationContract]
        DeviceDataContract LoadDeviceList();
    }

    [DataContract]
    public class DeviceDataContract
    {
        public DeviceDataContract() { }

        [DataMember]
        public DeviceCreate deviceCreate { get; set; }

        [DataMember]
        public DeviceDetails deviceDetails { get; set; }

        [DataMember]
        public DeviceEdit deviceEdit { get; set; }

        [DataMember]
        public int deviceDelete { get; set; }

        [DataMember]
        public DeviceList devicelist { get; set; }

    }
}