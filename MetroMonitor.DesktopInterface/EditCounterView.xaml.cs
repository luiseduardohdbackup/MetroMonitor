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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MetroMonitor.DesktopInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCounterView : Page
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        private int SelectedDevice;

        public EditCounterView()
        {
            this.InitializeComponent();
            LoadDeviceDropDownContent();
        }

        private async void LoadDeviceDropDownContent()
        {

            var deviceData = await dataClient.GetAvailableDevicesAsync();

            foreach (var r in deviceData)
            {
                DeviceListDD.Items.Add(new ComboBoxItem
                {
                    Content = r.Value,
                    Name = r.Key.ToString(),
                    DataContext = r.Key

                });
            }
        }

        private async void LoadSelectedDeviceCountersDownContent(int deviceId)
        {
            DeviceCountersDD.Visibility = Windows.UI.Xaml.Visibility.Visible;
            SelectedDevice = deviceId;
            var counterData = await dataClient.GetAvailableCountersForDeviceAsync(deviceId);

            foreach (var r in counterData)
            {
                DeviceCountersDD.Items.Add(new ComboBoxItem
                {
                    Content = r.Value,
                    Name = r.Key.ToString(),
                    DataContext = r.Key

                });
            }
        }

        private async void LoadSelectedCounterContent(int deviceCounterId, int selectedDevice)
        {
           
            var counterData = await counterClient.GetMetricDetailsAsync(selectedDevice, deviceCounterId);

            ReadInterTB.DataContext = counterData.MetricDetails.Counter.ReadInterval;
            LogInterTB.DataContext = counterData.MetricDetails.Counter.LogInterval;
            MaxThresTB.DataContext = counterData.MetricDetails.Counter.MaxThreshold;
            MinThresTB.DataContext = counterData.MetricDetails.Counter.MinThreshold;

            ReadInterTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
            LogInterTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MaxThresTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MinThresTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ConfirmBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;

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
            var data = (ComboBoxItem) selectedDevice;

            LoadSelectedDeviceCountersDownContent((int)data.DataContext);
        }

        private void DeviceCountersDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDevice = DeviceCountersDD.SelectedItem;
            var data = (ComboBoxItem)selectedDevice;

            LoadSelectedCounterContent((int)data.DataContext, SelectedDevice);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
