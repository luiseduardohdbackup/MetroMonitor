using System.Data.Entity;
using MetroMonitor.Entities;

namespace MetroMonitor
{
    public class MetroMonitorContext : DbContext, IMetroMonitorContext
    {
        public MetroMonitorContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Result> Results { get; set; }
        public IDbSet<Device> Devices { get; set; }
        public IDbSet<DeviceCounterBase> DeviceCounters { get; set; }
        public IDbSet<PerformanceCategory> PerformanceCategories { get; set; }
        public IDbSet<PerformanceCounter> PerformanceCounters { get; set; }
        public IDbSet<PerformanceCounterDefinition> PerformanceCounterDefinitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceCounterBase>().ToTable("DeviceCounters");
            modelBuilder.Entity<DeviceCounterBase>().HasRequired(c => c.Device);
            modelBuilder.Entity<DevicePerformanceCounter>().ToTable("DevicePerformanceCounters");
            modelBuilder.Entity<DevicePerformanceCounter>().Ignore(d => d.Category);
            modelBuilder.Entity<DevicePerformanceCounter>().Ignore(d => d.Name);
            modelBuilder.Entity<Result>().HasRequired(r => r.DeviceCounter);
            modelBuilder.Entity<Device>().ToTable("Device");
        }
    }
}