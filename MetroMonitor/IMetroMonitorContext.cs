using System;
using System.Data.Entity;
using MetroMonitor.Entities;

namespace MetroMonitor
{
    public interface IMetroMonitorContext : IDisposable
    {
        IDbSet<Result> Results { get; set; }
        IDbSet<Device> Devices { get; set; }
        IDbSet<DeviceCounterBase> DeviceCounters { get; set; }
        IDbSet<PerformanceCategory> PerformanceCategories { get; set; }
        IDbSet<PerformanceCounter> PerformanceCounters { get; set; }
        IDbSet<PerformanceCounterDefinition> PerformanceCounterDefinitions { get; set; }
        int SaveChanges();
    }
}