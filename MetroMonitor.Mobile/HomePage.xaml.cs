using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MetroMonitor.Mobile
{
    public partial class HomePage : PhoneApplicationPage
    {
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
        
        public HomePage()
        {
            InitializeComponent();

            //dataClient.GetAvailableDevicesCompleted += dataClient_GetAvailableDevicesCompleted;
            //dataClient.GetAvailableDevicesAsync();
        }

        //void dataClient_GetAvailableDevicesCompleted(object sender, MobileDataRepo.GetAvailableDevicesCompletedEventArgs e)
        //{
        //    var data = new List<ListBoxItem>();

        //    foreach(var item in e.Result){
            
        //        var lbi = new ListBoxItem{Content = item.Value,
        //            DataContext = item.Key};
        //        data.Add(lbi);
        //    }

        //   DeviceNameListBox.ItemsSource = data;
        //}

        //private void DeviceNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selectedItem = DeviceNameListBox.SelectedItem;
        //    var item = (ListBoxItem)selectedItem;


        //    string uri = "/CounterSelect.xaml?Text=";
        //    uri += item.DataContext.ToString();
        //    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        //    this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            
        //}

      
    

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
            
        //    this.NavigationService.Navigate(new System.Uri(@"/CounterSelect.xaml", UriKind.RelativeOrAbsolute));
        
        //}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string uri = "/DeviceStatus.xaml";
           
           // NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string uri = "/DeviceSelect.xaml";

            // NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            this.NavigationService.Navigate(new Uri(uri, UriKind.Relative));

        }

    
    }
}