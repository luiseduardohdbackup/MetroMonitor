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
    public sealed partial class ManageDevice : Page
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        private int SelectedDevice;

        public ManageDevice()
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

        private void LoadEditAndDeleteUI(string deviceName)
        {

            DeviceDeleteBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DeviceNameTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DeviceNameTB.Text = deviceName;
        }

        private async void ProcessDeviceDelete()
        {
            var data = await deviceClient.DeleteDeviceAsync(SelectedDevice);

            //TODO: Implement 
        }

        private async void ProcessDeviceAdd()
        {
            var data = await deviceClient.AddDeviceAsync(AddDeviceTB.Text.ToString());

            //TODO: Implement 
        }

        private async void ProcessDeviceEdit()
        {
            var data = await deviceClient.EditDeviceAsync(DeviceNameTB.Text, SelectedDevice);
            if (data)
            {

                //TODO: Implement 
            }
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
            var UIDataItem = DeviceListDD.SelectedItem;
            var selectedComboBoxItem = (ComboBoxItem)UIDataItem;
            SelectedDevice = (int)selectedComboBoxItem.DataContext;
            LoadEditAndDeleteUI(selectedComboBoxItem.Content.ToString());

        }

        private void AddDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
              AddDeviceTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
              AddBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessDeviceAdd();
        }
    }
}
