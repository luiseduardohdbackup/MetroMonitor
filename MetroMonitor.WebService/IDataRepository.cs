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

namespace MetroMonitor.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
     [ServiceContract]
     public interface IDataRepository
     {
       
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetResult();

        [OperationContract]
        CompositeType GetTestData();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
     }
 

    // Use a data contract as illustrated in the sample below to add composite types to service operations.

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        public CompositeType()
        {
            ResultValue = null;
        }

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        [DataMember]
        public ResultsData ResultValue { get; set; }
    }
}
