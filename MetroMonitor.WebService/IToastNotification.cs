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
using MetroMonitor.DataServices;
using MetroMonitor.WebService.DataMembers;


namespace MetroMonitor.WebService
{
    [ServiceContract]
    public interface IToastNotification
    {
        [OperationContract]
        [WebGet]
        string SendNotification(string message);
    }
}