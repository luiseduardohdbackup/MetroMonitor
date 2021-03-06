﻿using System;
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

namespace MetroMonitor.WebService
{
    [DataContract]
    public class GraphDataContract
    {
        
        public GraphDataContract() { }

        [DataMember]
        public Dictionary<GraphCounterDataContract, List<ResultsDataContract>> PlottingData { get; set; }

      

    }

}