using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MetroMonitor.Entities;
using MetroMonitor.DataServices;
using MetroMonitor.IntergrationTest;
using MetroMonitor.ViewModels.Devices;
using MetroMonitor.ViewModels.Counters;
using MetroMonitor.ViewModels.Results;
using Moq;
using System.Configuration;
using NUnit.Framework;

namespace MetroMonitor.IntergrationTest
{
    [TestFixture]
    public class StatisticsServiceTest
    {
        private StatisticsProcessingService _Context;
      
        [SetUp]
        public void SetUp()
        {
            _Context = new StatisticsProcessingService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
        }

        [Test]
        public void GetCounterSummaryStatus_ReturnsMetricStatusResult()
        {
            //  var devices = _context.Setup(ds => ds.GetAvailableDevice).Returns(new Dictionary<int,string>());

            var result = _Context.GetCounterSummaryStatus(1);

            Assert.IsNotNull(result, "Instance from Statistics Processing Service");
            Assert.That(result, Is.InstanceOf<DeviceMetricStatusResult>());
        }
    }
}
