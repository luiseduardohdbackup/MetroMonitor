using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroMonitor.ViewModels.Results;

namespace MetroMonitor.DataServices
{
    public interface IStatisticsProcessingService
    {
        IEnumerable<DeviceStatusResult> GetSystemDeviceStatusList();

        DeviceMetricStatusResult GetCounterSummaryStatus(int id);

        //DeviceAvailabilityCollection GetDeviceAvailabilityView();

        //ThresholdBreachedSystemsCollection GetThresholdBreachedSystemDeviceStatusList();

        StatusData.Status GetCurrentStatus(int counterId);
    }
}
