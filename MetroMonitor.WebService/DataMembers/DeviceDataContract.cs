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
using MetroMonitor.ViewModels.Statistics;

namespace MetroMonitor.WebService.DataMembers
{
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