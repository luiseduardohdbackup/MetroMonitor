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
using MetroMonitor.DesktopInterface.MetroMonitorWebRepository;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MetroMonitor.DesktopInterface
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddCounterView : MetroMonitor.DesktopInterface.Common.LayoutAwarePage
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        public AddCounterView()
        {
          
            this.InitializeComponent();
            CounterToAddDD.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            LoadDeviceDropDownContent();
        }

        private async void LoadDeviceDropDownContent() { 

            var deviceData = await dataClient.GetAvailableDevicesAsync();

            foreach(var r in deviceData){
                DeviceNameDD.Items.Add(new ComboBoxItem
                {
                    Content = r.Value,
                    Name = r.Key.ToString(),
                    DataContext = r.Key
                    
                });
            }
        
        }

        private void GenerateReadIntervalUI() {
            for (int i = 0; i < 20; i++)
            {
                ReadIntervalDD.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }

            ReadIntervalDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        private void GenerateLogIntervalUI()
        {
            for (int i = 0; i < 20; i++)
            {
                LogIntervalDD.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }

            LogIntervalDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        private void GenerateMaxThresholUI()
        {
            for (int i = 0; i < 20; i++)
            {
                MaxThresholdDD.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }

            MaxThresholdDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        private void GenerateMinThresholdUI()
        {
            for (int i = 0; i < 20; i++)
            {
                MinThresholdDD.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }

            MinThresholdDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        private void GenerateConfirmationButtonUI() {
            ConfirmationButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        //private static int GetComboBoxData(this ComboBox data) {
        //    var item = data.SelectedItem;
        //    var dataItem = (ComboBoxItem)item;
        //    return (int)dataItem.DataContext;
        
        //}

        private void SerializeFormData() {

            var d = DeviceNameDD.SelectedItem;
            var e = (ComboBoxItem)d;

            var c = CounterToAddDD.SelectedItem;
            var sc = (ComboBoxItem)c;

            testarea1.Text = e.Content + " " + e.DataContext;

            counterClient.AddMetricAsync(new CounterCreate
            {
                DeviceName = e.Content.ToString(),
                DeviceId = (int)e.DataContext,
                CounterDefinitifionId = (int)sc.DataContext,

                Metric = new CounterBase {
                Description = string.Empty,
                LogInterval = 0,
                MaxThreshold =0,
                MinThreshold =0,
                ReadInterval =0
                }
            });
            
        }

        private async void getCounterList() {

            CounterToAddDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

            var r = await counterClient.LoadAvailableCountersAsync();

            foreach (var result in r)
            {
                CounterToAddDD.Items.Add(new ComboBoxItem
                {
                    DataContext = result.Key,
                    Name = result.Key.ToString(),                    
                    Content = result.Value

                });
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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            testarea1.Text += e.AddedItems.ToString(); 
            GenerateReadIntervalUI();
        }
               
        private void DeviceNameDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getCounterList();
        }

        private void ReadIntervalDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateLogIntervalUI();
        }

        private void LogIntervalDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateMaxThresholUI();

        }

        private void MaxThresholdDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateMinThresholdUI();
        }

        private void MinThresholdDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateConfirmationButtonUI();
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            testarea1.Text += sender.ToString() + e.ToString();
            SerializeFormData();
        }
    }
}
