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
using Moq;
using System.Configuration;
using NUnit.Framework;

namespace MetroMonitor.IntergrationTest
{
     [TestFixture]
    public class DataAccessServiceTest
    {

        private Mock<IDataAccessService> _context;
        private DataAccessService dataContext;
      
        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataAccessService>();
            dataContext = new DataAccessService(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
          
         
        }

        [Test]
        public void GetAvailableDevices_ReturnsDeviceList()
        {
          //  var devices = _context.Setup(ds => ds.GetAvailableDevice).Returns(new Dictionary<int,string>());
            
            var result = dataContext.GetAvailableDevice();

            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.EqualTo(new Dictionary<int, string>()));
        }

        [Test]
        public void GetAvaialbeCounters_ReturnsAvailableCountersList()
        {
            var result = dataContext.GetAvaialbeCounters();
            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.EqualTo(new Dictionary<int, string>()));
        }

        [Test]
        public void AddNewDevice_AddsDevice()
        {
            var result = dataContext.AddNewDevice("Test Device");
            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void GetDeviceEdit_ReturnsEditableDevice()
        {
            var result = dataContext.GetDeviceEdit(1);
            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.InstanceOf<DeviceEdit>());
        }

        [Test]
        public void LoadCounterList_ReturnsCounterList_ForSpesificDevice()
        {
            var result = dataContext.LoadCounterList(1);
            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.EqualTo(new List<CounterDetails>()));
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetMetricDetails_ReturnsCounterDetailst_ForSpesificCounter()
        {
            var result = dataContext.GetMetricDetails(1, 1);
            Assert.IsNotNull(result, "Instance from Data Access Service");
            Assert.That(result, Is.InstanceOf<CounterDetails>());
        }
        #region Helpers

        private static List<Device> CreateDeviceList()
        {
            return new List<Device>
                       {
                           new Device
                               {
                                   Id = 1,
                                   Name = "My Device",
                                   Counters = CreateCounters()
                               }
                       };
        }

        private static List<DeviceCounterBase> CreateCounters()
        {
            return new List<DeviceCounterBase>
                       {
                           new DevicePerformanceCounter
                               {
                                   CounterDefinition = new PerformanceCounterDefinition
                                                           {
                                                               Category = new PerformanceCategory{Name = "Test"},
                                                               Counter = new PerformanceCounter{Name = "Other"}
                                                           }
                               },
                          
                       };
        }



        #endregion
    }
}

   