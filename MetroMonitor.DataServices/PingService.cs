using System;
using System.Net.NetworkInformation;

namespace MetroMonitor.DataServices
{
    public static class PingService
    {
        public static bool IsValidDeviceName(string deviceName)
        {
            if (deviceName == null) throw new ArgumentNullException("deviceName");
            var pingInstance = new Ping();
            var pingResponce = pingInstance.Send(deviceName);
            return pingResponce != null && pingResponce.Status == IPStatus.Success;
        }
    }
}
