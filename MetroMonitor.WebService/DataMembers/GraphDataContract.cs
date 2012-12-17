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

namespace MetroMonitor.WebService
{
    [DataContract]
    public class GraphDataContract
    {
        //TODO: COME BACK TO THIS SHIT
        public GraphDataContract() { }

        [DataMember]
        public GraphData PlottingData { get; set; }

        [DataMember]
        public List<Result> resultSet { get; set; }


    }

    //implement new data members containing basic data types to serialize
}