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
    public class GraphCounterDataContract
    {
        public GraphCounterDataContract() {}

        [DataMember]
        public string CounterDescription { get; set; }

        [DataMember]
        public string InstanceName { get; set; }

    }
}