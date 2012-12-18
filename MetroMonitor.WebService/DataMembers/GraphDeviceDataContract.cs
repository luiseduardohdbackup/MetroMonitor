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
using MetroMonitor.DataServices;

namespace MetroMonitor.WebService.DataMembers
{
    [DataContract]
    public class GraphDeviceDataContract
    {
        [DataMember]
        public string DeviceName { get; set; }
    }
}