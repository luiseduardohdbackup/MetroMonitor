using System.Data.SqlClient;
using MetroMonitor.Entities;

namespace MetroMonitor.DataServices.Extensions
{
    public static class DeviceCounterBaseExtensions
    {
        public static string GetDescription(this DeviceCounterBase counter)
        {
            var actual = counter as DevicePerformanceCounter;
            if (actual != null)
            {
                if (!string.IsNullOrEmpty(actual.InstanceName))
                {
                    return string.Format("{1}-{2} ({0})", actual.InstanceName, actual.Category, actual.Name);
                }
                return string.Format("{0}-{1} Performance Counter",
                                     actual.Category,
                                     actual.Name);
            }

            return "Unknown Counter Type";
        }
    }
}