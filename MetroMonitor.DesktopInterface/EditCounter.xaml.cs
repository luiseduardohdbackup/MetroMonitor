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
    public sealed partial class EditCounter : MetroMonitor.DesktopInterface.Common.LayoutAwarePage
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        private int SelectedDevice;

        private int SelectCounter;

        public EditCounter()
        {
            this.InitializeComponent();
            LoadDeviceDropDownContent();
        }

        private async void LoadDeviceDropDownContent()
        {

            var deviceData = await dataClient.GetAvailableDevicesAsync();

            var devices = new List<TextBlock>();


            foreach (var r in deviceData)
            {
                devices.Add(new TextBlock
                {
                   Text = r.Value.ToString(),
                    Name = r.Key.ToString(),
                    DataContext = r.Key

                });
            }
            DeviceListDD.ItemsSource = devices;
        }

        private async void LoadSelectedDeviceCountersDownContent(int deviceId)
        {
            DeviceCountersDD.Visibility = Windows.UI.Xaml.Visibility.Visible;
            SelectedDevice = deviceId;
            var counterData = await dataClient.GetAvailableCountersForDeviceAsync(deviceId);
            var counters = new List<TextBlock>();

            foreach (var r in counterData)
            {
                counters.Add(new TextBlock
                {
                    Text = r.Value,
                    Name = r.Key.ToString(),
                    DataContext = r.Key

                });
            }
            DeviceCountersDD.ItemsSource = counters;
        }

        private void GenerateComboBoxContent(ComboBox dropDown, TextBlock textblock, int currentValue)
        {
           
           
            dropDown.Items.Add(new ComboBoxItem { Name = currentValue.ToString(), DataContext = currentValue, Content = currentValue });

            for (int i = 0; i < 20; i++)
            {
                dropDown.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }
            dropDown.SelectedIndex = 0;

            dropDown.Visibility = Windows.UI.Xaml.Visibility.Visible;
            textblock.Visibility = Windows.UI.Xaml.Visibility.Visible;
                
        

        }
        private async void LoadSelectedCounterContent(int deviceCounterId, int selectedDevice)
        {
            SelectCounter = deviceCounterId;
            var counterData = await counterClient.GetMetricDetailsAsync(deviceCounterId, selectedDevice);
          
            if (ReadInterTB.Visibility != Windows.UI.Xaml.Visibility.Collapsed)
            {
                ReadInterTB.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ReadInterTxt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                LogInterTB.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                LogInterTxt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                MaxThresTB.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MaxThresTxt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                MinThresTB.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MinThresTxt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            GenerateComboBoxContent(ReadInterTB, ReadInterTxt, counterData.MetricDetails.Counter.ReadInterval);
            GenerateComboBoxContent(LogInterTB, LogInterTxt, counterData.MetricDetails.Counter.LogInterval);
            GenerateComboBoxContent(MaxThresTB, MaxThresTxt, counterData.MetricDetails.Counter.MaxThreshold);
            GenerateComboBoxContent(MinThresTB, MinThresTxt, counterData.MetricDetails.Counter.MinThreshold);

        }

        private async void ProcessCounterEdit() {

            var ri = ReadInterTB.SelectedItem;
            var e = (ComboBoxItem)ri;

            var li = LogInterTB.SelectedItem;
            var sc = (ComboBoxItem)li;

            var mt = MaxThresTB.SelectedItem;
            var ricb = (ComboBoxItem)mt;

            var mint= MinThresTB.SelectedItem;
            var licb = (ComboBoxItem)mint;

            var counterUpdated = await counterClient.EditMetricAsync(SelectCounter, (int)e.DataContext, (int)sc.DataContext, (int)ricb.DataContext, (int)licb.DataContext);

            UpdateStatusTB.Text = counterUpdated.ToString();
        
        
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DeviceListDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDevice = DeviceListDD.SelectedItem;
            var data = (TextBlock) selectedDevice;

            LoadSelectedDeviceCountersDownContent((int)data.DataContext);
        }

        private void DeviceCountersDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDevice = DeviceCountersDD.SelectedItem;
            var data = (TextBlock)selectedDevice;

            LoadSelectedCounterContent((int)data.DataContext, SelectedDevice);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessCounterEdit();
        }

       
    }
}
