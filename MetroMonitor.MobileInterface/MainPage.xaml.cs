using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MetroMonitor.MobileInterface.Resources;

namespace MetroMonitor.MobileInterface
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
        
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            GenerateData();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void GenerateData()
        {
            dataClient.GetAvailableDevicesCompleted += dataClient_GetAvailableDevicesCompleted;

            dataClient.GetAvailableDevicesAsync();

     

            //var data = dataClient.GetAvailableDevicesAsync();


        }

        void dataClient_GetAvailableDevicesCompleted(object sender, MobileDataRepo.GetAvailableDevicesCompletedEventArgs e)
        {
            devicestextbox.Text += "hit method";
            foreach (var data in e.Result)
            {
                devicestextbox.Text += data.Value.ToString();
            }
        }

        void dataClient_GetAvailableCountersForDeviceCompleted(object sender, MobileDataRepo.GetAvailableCountersForDeviceCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}