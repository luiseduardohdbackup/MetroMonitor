using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MetroMonitor.DesktopInterface
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DeviceStatuses : MetroMonitor.DesktopInterface.Common.LayoutAwarePage
    {
        public class DeviceStatusListView {
            public string DeviceName{get; set;}
            public SolidColorBrush Status { get; set; }
        }

        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();



        MetroMonitorWebRepository.StatisticsContractClient StatisticsClient = new MetroMonitorWebRepository.StatisticsContractClient();

        public DeviceStatuses()
        {
            this.InitializeComponent();
            AddDevicesToListView();
        }

        private async void AddDevicesToListView() {

            var statuses = await StatisticsClient.GetStatusesForAllDevicesAsync();
            var listSource = new List<TextBlock>();
            foreach (var data in statuses) {
                var tb = new TextBlock();
                tb.Text = data.DeviceName + data.Status;
                tb.DataContext = data.Id;

                listSource.Add(tb);
            
            }

            DSList.ItemsSource = listSource;
        }

        private async void loadCounterStatusesFromDevice(int deviceId) {
            var listview = new ListView();
            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceId);
            foreach(var d in data.Statistics){
                counterStatusesTB.Text += d.CounterName + " ";

                foreach (var stats in d.TimeFrameResult) {

                    counterStatusesTB.Text += stats.Status.ToString();
                }
            
            }
         
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void DeviceStatusListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = DSList.SelectedItem;
            var selectedData = (TextBlock)list;
            loadCounterStatusesFromDevice((int)selectedData.DataContext);

        }
    }
}
