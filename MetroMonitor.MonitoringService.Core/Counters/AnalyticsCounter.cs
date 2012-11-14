using System;
using System.Timers;
using MetroMonitor.Entities;
using MetroMonitor.MonitoringService.Core.Timers;

namespace MetroMonitor.MonitoringService.Core.Counters
{
    public delegate void ResultEventHandler(object sender, ResultEventArgs e);

    public abstract class AnalyticsCounter
    {
        private readonly DeviceCounterBase _deviceCounter;
        private readonly IIntervalTimer _timer;
        private DateTime _nextLog;
        public event ResultEventHandler ResultGenerated;


        //New Implementation
        private readonly int _numberOfReads;
        private int _readCount = 0;


        protected static readonly object SyncLock = new object();

        protected AnalyticsCounter(DeviceCounterBase deviceCounter)
            : this(deviceCounter, new IntervalTimer())
        {

        }

        internal AnalyticsCounter(DeviceCounterBase deviceCounter, IIntervalTimer timer)
        {
            _deviceCounter = deviceCounter;
            _timer = timer;
            _numberOfReads = deviceCounter.LogInterval / deviceCounter.ReadInterval;
            _timer.Interval = (deviceCounter.LogInterval / _numberOfReads) * 1000;
        }

        protected void LogResult(int intervals, double min, double max, double average)
        {
            var handler = ResultGenerated;
            if (handler == null) return;

            var result = new Result
            {
                DeviceCounter = _deviceCounter,
                LogDate = DateTime.Now,
                Intervals = intervals,
                MinimumRead = min,
                MaximumRead = max,
                AverageRead = average
            };

            var e = new ResultEventArgs(result);
            handler(this, e);
        }

        public void Start()
        {
            _readCount = 0;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TakeReading();

            if (++_readCount < _numberOfReads)
                return;

            NotifyResult();
        }

        protected abstract void TakeReading();
        protected abstract void NotifyResult();

        public void Stop()
        {
            _timer.Stop();
            LogResult(0, 0, 0, 0);
            _timer.Elapsed -= TimerElapsed;
        }
    }
}