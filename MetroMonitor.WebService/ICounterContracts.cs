using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;
using MetroMonitor.WebService.DataMembers;

namespace MetroMonitor.WebService
{
    [ServiceContract]
    public interface ICounterContracts
    {                   
        [OperationContract]
        bool AddMetric(CounterCreate counter);

        [OperationContract]
        bool DeleteMetric(int id);

        [OperationContract]
        bool EditMetric(EditCounter counter);

        [OperationContract]
        CounterDataContract MetricDetails(int counterId, int deviceId);

        [OperationContract]
        CounterDataContract LoadMetricList(int deviceId);
    }

   

}