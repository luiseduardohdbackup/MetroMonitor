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
        bool AddNewDevice(string deviceName);
        bool DeleteDevice(int id);
        bool EditDevice(string deviceName, int Id);
        DeviceList LoadDeviceList();
        DeviceEdit GetDeviceEdit(int id);
        Dictionary<int, string> GetAvaialbeCounters();
        DeviceDetails GetDeviceDetails(int id);
        Dictionary<int, string> GetAvailableDevice();
        Dictionary<int, string> GetCountersForDevice(int deviceId);
      
        //Generic Data Operations
        ResultsData GetResults();

        //Counter Opertions Method Contracts
        
        EditCounter GetCounterEditView(int id);
        IList<CounterDetails> LoadCounterList(int deviceId);
        CounterCreate GetMetricCreateView(int id, CounterCreate type);
        CounterDelete LoadCounterToDelete(int id);
        CounterDetails GetMetricDetails(int deviceId, int counterId);
        bool DeleteMetric(int id);
        bool AddNewMetric(CounterCreate metric);
        bool UpdateMetric(int counterID, int read, int log, int min, int max);

        IEnumerable<CounterInstancesForInterface> GetCounterList();

    }
}
