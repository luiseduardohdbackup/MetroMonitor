using MetroMonitor.DesktopInterface.Common;
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
    public sealed partial class ManageDevice : MetroMonitor.DesktopInterface.Common.LayoutAwarePage
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        private int SelectedDevice = 0;

        public ManageDevice()
        {
            this.InitializeComponent();
            LoadDeviceDropDownContent();
        }

        private async void LoadDeviceDropDownContent()
        {
            if (DeviceListDD.Items != null)
            {
                DeviceListDD.SelectedIndex = -1;
                DeviceListDD.Items.Clear();
            }
            var deviceData = await dataClient.GetAvailableDevicesAsync();

            foreach (var r in deviceData)
            {
                DeviceListDD.Items.Add(new ListBoxItem
                {
                   Content = r.Value,
                    Name = r.Key.ToString(),
                    DataContext = r.Key

                });
            }
        }

        private void LoadEditAndDeleteUI(string deviceName)
        {
            if (DeviceDeleteBtn.Visibility != Windows.UI.Xaml.Visibility.Collapsed)
            {
                DeviceDeleteBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DeviceNameTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ChangeDeviceNameBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            DeviceNameTB.Text = deviceName;
        }

        private async void ProcessDeviceDelete()
        {
            var data = await deviceClient.DeleteDeviceAsync(SelectedDevice);
            if (data)
            {
                DeviceEditStatus.Text = "Device Has Been Sucessfully Deleted";
            }
            LoadDeviceDropDownContent();
        }

        private async void ProcessDeviceAdd()
        {
            var data = await deviceClient.AddDeviceAsync(AddDeviceTB.Text.ToString());
            if (data) {
                AddDeviceStatusTB.Text = "Device Sucessfully Added";
            }
            LoadDeviceDropDownContent();
        }

        private async void ProcessDeviceEdit()
        {
            var data = await deviceClient.EditDeviceAsync(DeviceNameTB.Text, SelectedDevice);
            if (data)
            {
                if (EditDeviceTextBlock.Visibility != Windows.UI.Xaml.Visibility.Collapsed)
                    DeviceEditStatus.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DeviceEditStatus.Text = "Device Has Been Sucessfully Edited";
                
            }
            LoadDeviceDropDownContent();
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
            if (UIDataItem == null) {
               
                return;
            }
                        var selectedComboBoxItem = (ListBoxItem)UIDataItem;
            SelectedDevice = (int)selectedComboBoxItem.DataContext;
            LoadEditAndDeleteUI(selectedComboBoxItem.Content.ToString());

        }
       
        private void AddDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessDeviceAdd();
           
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessDeviceAdd();
        }

        private void ChangeDeviceNameBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessDeviceEdit();
        }

        private void DeviceDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessDeviceDelete();
        }

        private void BackNavigation(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GroupedItemsPage), null);
        }

    }
}
