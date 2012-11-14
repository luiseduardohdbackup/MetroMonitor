using System.ComponentModel;
using System.Timers;

namespace MetroMonitor.MonitoringService.Core.Timers
{
    public interface IIntervalTimer
    {
        void Start();
        void Stop();
        void Close();

        bool AutoReset { get; set; }
        bool Enabled { get; set; }
        double Interval { get; set; }
        ISynchronizeInvoke SynchronizingObject { get; set; }

        event ElapsedEventHandler Elapsed;
    }
}