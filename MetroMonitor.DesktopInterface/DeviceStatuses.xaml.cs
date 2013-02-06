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
using Windows.UI.Xaml.Shapes;



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

            var grid = new List<Grid>(statuses.Count);

            //var listSource = new List<TextBlock>();
            //var rectang = new List<Rectangle>();
           
            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

         
            
            foreach (var data in statuses) {
                //var tb = new TextBlock();
                //tb.Text = data.DeviceName + data.Status;
                //tb.DataContext = data.Id;
                if (data.Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }
                if (data.Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }
                var g = new Grid();
                g.Children.Add(new Rectangle{Fill = colour});
                g.Children.Add(new TextBlock{Text = data.DeviceName + data.Status,
                    DataContext = data.Id});

                //listSource.Add(tb);
                grid.Add(g);
            
            }

            DSList.ItemsSource = grid;
         //   DSList.ItemsSource = listSource;
        }

        private async void loadCounterStatusesFromDevice(int deviceId) {
            //var listview = new ListView();
            
            //foreach(var d in data.Statistics){
            //    counterStatusesTB.Text += d.CounterName + " ";

            //    foreach (var stats in d.TimeFrameResult) {

            //        counterStatusesTB.Text += stats.Status.ToString();
            //    }
            
            //}
            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceId);

            var grid = new List<Grid>();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            foreach (var d in data.Statistics)
            {

                foreach (var stats in d.TimeFrameResult)
                {
                    
                if (stats.Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

                if (stats.Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }

                var g = new Grid();
                g.Children.Add(new Rectangle { Fill = colour });
                g.Children.Add(new TextBlock
                {
                    Text = d.CounterName.ToString(),
                    DataContext = d.Id.ToString()
                });

                grid.Add(g);

                    counterStatusesTB.Text += stats.Status.ToString();
                }
            }

            CSList.ItemsSource = grid;
         
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
            var selectedData = (Grid)list;
          //  var t = selectedData.GetValue(
       //    var tb = (from t in selectedData.Children where t.Equals(typeof(TextBlock)) select t).FirstOrDefault();

            var tb = (TextBlock)selectedData.Children[1];
           // var txtb =(TextBlock) tb;
            loadCounterStatusesFromDevice((int)tb.DataContext);

        }
    }
}
