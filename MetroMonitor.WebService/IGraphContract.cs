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
    [ServiceContract]
    public interface IGraphContract
    {
        [OperationContract]
        GraphDataContract MetricsOverveiwForDevice(int deviceId);

        [OperationContract]
        GraphDataContract MetricsOverveiwForGraphForCounter(int deviceId, int counterId);


        [OperationContract]
        GraphDataContract GetResultsSet (int deviceId);

        [OperationContract]
        int TestService();
    }

  
}