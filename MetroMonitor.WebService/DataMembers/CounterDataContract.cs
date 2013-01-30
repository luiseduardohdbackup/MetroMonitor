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
using MetroMonitor.ViewModels.Counters;

namespace MetroMonitor.WebService.DataMembers
{
    [DataContract]
    public class CounterDataContract
    {
        public CounterDataContract() { }

        [DataMember]
        public IEnumerable<CounterComboBox> ComboBoxData { get; set; }
        
        [DataMember]
        public CounterDetails MetricDetails { get; set; }


        [DataMember]
        public IList<CounterDetails> MetricDetailsList { get; set; }
    }


    [DataContract]
    public class MetricDetailsData
    {
        public MetricDetailsData() { }

        [DataMember]
        public CounterDetails MetricDetails { get; set; }
    }

    [DataContract]
    public class CounterComboBox {

        [DataMember]
        public string Counter { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string InstanceName { get; set; }

    }

   
}