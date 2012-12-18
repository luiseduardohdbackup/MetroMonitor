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
using MetroMonitor.WebService.DataMembers;


namespace MetroMonitor.WebService.DataMembers
{
    [DataContract]
    public class ResultsDataContract
    {
        public ResultsDataContract() { }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime LogDate { get; set; }
        [DataMember]
        public int Intervals { get; set; }
        [DataMember]
        public double MinimumRead { get; set; }
        [DataMember]
        public double MaximumRead { get; set; }
        [DataMember]
        public double AverageRead { get; set; }
    }
}