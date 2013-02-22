using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MetroMonitor.Mobile
{
    public partial class CounterSelect : PhoneApplicationPage
    {
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();

        private string deviceId;

        public CounterSelect()
        {
            InitializeComponent(); dataClient.GetAvailableDevicesCompleted += dataClient_GetAvailableDevicesCompleted;
            dataClient.GetAvailableDevicesAsync();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("Text"))
            {
                GenerateCounterCounterDropDown(parameters["Text"]);
                deviceId = parameters["Text"];
            }
        }

        void dataClient_GetAvailableDevicesCompleted(object sender, MobileDataRepo.GetAvailableDevicesCompletedEventArgs e)
        {
            var data = new List<ListBoxItem>();

            foreach (var item in e.Result)
            {

                var lbi = new ListBoxItem
                {
                    Content = item.Value,
                    DataContext = item.Key
                };
                data.Add(lbi);
            }

          //  DeviceNameListBox.ItemsSource = data;
        }

        private void DeviceNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  var selectedItem = DeviceNameListBox.SelectedItem;
          //  var item = (ListBoxItem)selectedItem;

            //GenerateCounterCounterDropDown((int)item.DataContext);
        }
        
        private void GenerateCounterCounterDropDown(string deviceID)
        {
            int convert = Convert.ToInt32(deviceID);
            dataClient.GetAvailableCountersForDeviceCompleted += dataClient_GetAvailableCountersForDeviceCompleted;
            dataClient.GetAvailableCountersForDeviceAsync(convert);
        }

        void dataClient_GetAvailableCountersForDeviceCompleted(object sender, MobileDataRepo.GetAvailableCountersForDeviceCompletedEventArgs e)
        {
            var data = new List<ListBoxItem>();

            foreach (var item in e.Result)
            {

                var lbi = new ListBoxItem
                {
                    Content = item.Value,
                    DataContext = item.Key
                };

                data.Add(lbi);
            }
            CounterListBox.ItemsSource = data;

        }

        private void CounterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = CounterListBox.SelectedItem;
            var item = (ListBoxItem)selectedItem;

            string uri = "/Charts.xaml?Text=";
            uri += item.DataContext.ToString() + " " + deviceId;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}