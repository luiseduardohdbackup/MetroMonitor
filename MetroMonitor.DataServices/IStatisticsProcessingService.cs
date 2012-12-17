using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.DataServices
{
    public interface IStatisticsProcessingService
    {
        //IEnumerable<DeviceStatusResult> GetSystemDeviceStatusList();

        //DeviceMetricStatusResult GetCounterSummaryStatus(int id);

        //DeviceAvailabilityCollection GetDeviceAvailabilityView();

        //ThresholdBreachedSystemsCollection GetThresholdBreachedSystemDeviceStatusList();

        Status GetCurrentStatus(int counterId);
    }
}
