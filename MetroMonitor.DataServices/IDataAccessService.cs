using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroMonitor.Entities;
using MetroMonitor.ViewModels;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Results;

namespace MetroMonitor.DataServices
{
    public interface IDataAccessService
    {
        //Device Operation Method Contracts
        bool AddNewDevice(DeviceCreate model);
        bool DeleteDevice(int id);
        bool EditDevice(DeviceEdit device);
        DeviceList LoadDeviceList();
        DeviceEdit GetDeviceEdit(int id);
        DeviceDetails GetDeviceDetails(int id);

      
        //Generic Data Operations
        ResultsData GetResults();

        //Counter Opertions Method Contracts
        
        EditCounter GetCounterEditView(int id);
        CounterDetails LoadCounterList(CounterCreate model);
        CounterCreate GetMetricCreateView(int id, CounterCreate type);
        CounterDelete LoadCounterToDelete(int id);
        CounterCreate GetMetricDetailsView(CounterCreate model);
        int DeleteMetric(int id);
        int AddNewMetric(CounterCreate metric);
        int UpdateMetric(EditCounter model);
     


    }
}
