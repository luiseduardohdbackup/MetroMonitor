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

        private int deviceID;
        
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
           
            
            foreach (var data in statuses) {
                var colour = new SolidColorBrush(Windows.UI.Colors.Red);
                //var tb = new TextBlock();
                //tb.Text = data.DeviceName + data.Status;
                //tb.DataContext = data.Id;
                if (data.Status.ToString() == "Green") {
                    colour.Color = Windows.UI.Colors.Green;
                }
                if (data.Status.ToString() == "Yellow") {
                    colour.Color = Windows.UI.Colors.Yellow; 
                }
                var g = new Grid();
                
                g.Children.Add(new Rectangle{Fill = colour,
                Stretch = Stretch.Fill});
                g.Children.Add(new TextBlock{Text = data.DeviceName,
                DataContext = data.Id,
                Margin = new Thickness(160,12,0,0),
                Height = 50,
                Width = 400});

                grid.Add(g);
            
            }

            DSList.ItemsSource = grid;
         //   DSList.ItemsSource = listSource;
        }

        private async void loadCounterStatusesFromDevice(int deviceId)
        {
           
            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceId);

            var grid = new List<Grid>();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            foreach (var d in data.Statistics)
            {
                    if (d.TimeFrameResult.ElementAt(0).Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

                    if (d.TimeFrameResult.ElementAt(0).Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }

                    var g = new Grid();

                    g.Children.Add(new Rectangle { Fill = colour });
                    g.Children.Add(new TextBlock
                    {
                        Text = d.CounterName.ToString(),
                        DataContext = d.Id.ToString(),
                        Margin = new Thickness(80, 12, 0, 0),
                        Height = 50,
                        Width = 400
                    });

                    grid.Add(g);

                  //  counterStatusesTB.Text += d.TimeFrameResult.ElementAt(0).Status.ToString();
            
            }   CSList.ItemsSource = grid;
        }

        private async void GenerateCurrentTrendUI(string countername) {

            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceID);
            var filter = (from d in data.Statistics where d.CounterName == countername select d).FirstOrDefault();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            if (filter.TimeFrameResult.ElementAt(0).Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

            if (filter.TimeFrameResult.ElementAt(0).Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }


            CurrentStatusGrid.Children.Add(new Rectangle { Fill = colour});
            CurrentStatusGrid.Children.Add(new TextBlock
            {
                Margin = new Thickness(24, 27, 0, 0),
                Text = "Now\n" + filter.TimeFrameResult.ElementAt(0).Trend.ToString(),
                FontSize = 15
            });

        }


        private async void Generate10TrendUI(string countername)
        {

            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceID);
            var filter = (from d in data.Statistics where d.CounterName == countername select d).FirstOrDefault();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            if (filter.TimeFrameResult.ElementAt(1).Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

            if (filter.TimeFrameResult.ElementAt(1).Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }


            _10StatusGrid.Children.Add(new Rectangle { Fill = colour });
            _10StatusGrid.Children.Add(new TextBlock
            {
                Margin = new Thickness(24, 27, 0, 0),
                Text = "10 Mins\n" + " " + filter.TimeFrameResult.ElementAt(1).Trend.ToString(),
                FontSize = 15
            });

        }
        private async void Generate20TrendUI(string countername)
        {

            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceID);
            var filter = (from d in data.Statistics where d.CounterName == countername select d).FirstOrDefault();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            if (filter.TimeFrameResult.ElementAt(2).Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

            if (filter.TimeFrameResult.ElementAt(2).Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }


            _20StatusGrid.Children.Add(new Rectangle { Fill = colour });
            _20StatusGrid.Children.Add(new TextBlock
            {
                Margin = new Thickness(24,27, 0,0),
                Text = "20 Mins\n" + filter.TimeFrameResult.ElementAt(2).Trend.ToString(),
                FontSize = 15
            });

        }
        private async void Generate30TrendUI(string countername)
        {

            var data = await StatisticsClient.GetCounterSummaryStatusAsync(deviceID);
            var filter = (from d in data.Statistics where d.CounterName == countername select d).FirstOrDefault();

            var colour = new SolidColorBrush(Windows.UI.Colors.Red);

            if (filter.TimeFrameResult.ElementAt(3).Status.ToString() == "Green") { colour.Color = Windows.UI.Colors.Green; }

            if (filter.TimeFrameResult.ElementAt(3).Status.ToString() == "Yellow") { colour.Color = Windows.UI.Colors.Yellow; }


            _30StatusGrid.Children.Add(new Rectangle { Fill = colour });
            _30StatusGrid.Children.Add(new TextBlock
            {
                Margin = new Thickness(24, 27, 0, 0),
                Text = "30 Mins\n" + filter.TimeFrameResult.ElementAt(3).Trend.ToString(),
                FontSize = 15
            });

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
         
            var tb = (TextBlock)selectedData.Children[1];

            deviceID = (int)tb.DataContext;
            loadCounterStatusesFromDevice((int)tb.DataContext);

        }

        private void CSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CSList.SelectedItem != null)
            {
                var list = CSList.SelectedItem;
                var selectedData = (Grid)list;

                var tb = (TextBlock)selectedData.Children[1];

                GenerateCurrentTrendUI(tb.Text);
                Generate10TrendUI(tb.Text);
                Generate20TrendUI(tb.Text);
                Generate30TrendUI(tb.Text);

            }
        }
    }
}
