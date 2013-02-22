using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Coding4Fun.Toolkit.Controls;

namespace MetroMonitor.Mobile
{
    public partial class DeviceSelect : PhoneApplicationPage
    {
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
        

        public DeviceSelect()
        {
            InitializeComponent();
            dataClient.GetAvailableDevicesCompleted += dataClient_GetAvailableDevicesCompleted;
            dataClient.GetAvailableDevicesAsync();

            ToastPrompt toast = new ToastPrompt();

            toast.Title = "ToastPrompt";
            toast.Message = "Some message";
            toast.FontSize = 50;
            toast.TextOrientation = System.Windows.Controls.Orientation.Vertical;
      
            toast.Completed += toast_Completed;
            toast.Show();
        }

        void toast_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            
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

            DeviceNameListBox.ItemsSource = data;
        }

        private void DeviceNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = DeviceNameListBox.SelectedItem;
            var item = (ListBoxItem)selectedItem;


            string uri = "/CounterSelect.xaml?Text=";
            uri += item.DataContext.ToString();
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));

           
        }

    }
}