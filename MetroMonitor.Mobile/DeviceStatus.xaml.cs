using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace MetroMonitor.Mobile
{
    public partial class DeviceStatus : PhoneApplicationPage
    {
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
        MobileDataRepo.StatisticsContractClient statisticsClient = new MobileDataRepo.StatisticsContractClient();   
        
        public DeviceStatus()
        {
            InitializeComponent();
                statisticsClient.GetStatusesForAllDevicesCompleted +=statisticsClient_GetStatusesForAllDevicesCompleted;
            statisticsClient.GetStatusesForAllDevicesAsync();
        }

        void statisticsClient_GetStatusesForAllDevicesCompleted(object sender, MobileDataRepo.GetStatusesForAllDevicesCompletedEventArgs e)
        {
            


            var statuslist = new List<ListBoxItem>();
            foreach (var data in e.Result)
            {

                var brush = new SolidColorBrush();

                brush.Color = Colors.Yellow;

                if (data.Status.ToString().Equals("Red"))
                {
                    brush.Color = Colors.Red;
                }

                if (data.Status.ToString().Equals("Green"))
                {
                    brush.Color = Colors.Green;
                }

                var lbi = new ListBoxItem
                {
                    Content = data.DeviceName,
                    DataContext = data.Id,
                    Background = brush
                };
                statuslist.Add(lbi);
            }

           
            DeviceStatusesListBox.ItemsSource = statuslist;
        }

        private void DeviceStatusesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = DeviceStatusesListBox.SelectedItem;
            var item = (ListBoxItem)selectedItem;


            string uri = "/CounterStatus.xaml?Text=";
            uri += item.DataContext.ToString();
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));

        }
      
    }
}