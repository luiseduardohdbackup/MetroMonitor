using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;

namespace MetroMonitor.MobileInterface
{
    public partial class HomePage : PhoneApplicationPage
    {

       MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
       MobileDataRepo.StatisticsContractClient statisticsDataClient = new MobileDataRepo.StatisticsContractClient();
        
        
        public HomePage()
        {
            InitializeComponent();
            GenerateData();
        }

        private void GenerateData() {

         
            statisticsDataClient.GetStatusesForAllDevicesCompleted += statisticsDataClient_GetStatusesForAllDevicesCompleted;
            statisticsDataClient.GetStatusesForAllDevicesAsync();    
        }

        void statisticsDataClient_GetStatusesForAllDevicesCompleted(object sender, MobileDataRepo.GetStatusesForAllDevicesCompletedEventArgs e)
        {
            var g = new List<Grid>();

            var deviceTextBlock = new List<TextBlock>();

          
           
            var colour = new SolidColorBrush(Colors.Red);

            foreach (var res in e.Result)
            {
               
                if (res.Status.ToString() == "Green")
                {
                    colour.Color = Colors.Green;
                }
                if (res.Status.ToString() == "Yellow")
                {
                    colour.Color = Colors.Yellow;
                }
                var text = new TextBlock
                {
                    Text = res.DeviceName + res.Status,
                    DataContext = res.Id,
                    
                    Padding = new System.Windows.Thickness { Top = 10 }

                };
                text.Tap+=text_Tap;

                deviceListStackPannel.Children.Add(text);

               
                deviceTextBlock.Add(new TextBlock
                {
                    Text = res.DeviceName + res.Status,
                   
                    
                    DataContext = res.Id,
                    Padding = new System.Windows.Thickness{Top = 10}
                    
                });

                
                
                
            }
           // statuslistSelector.ItemsSource = g;
        }

        void text_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock t = 
            MessageBox.Show(
        }

        private void GenertateStatusList()
        {

           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ChartsOutput.xaml", UriKind.Relative));

        }

        private void PivotItem_DoubleTap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void deviceListStackPannel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            

        }

    
        //void dataClient_GetAvailableCountersForDeviceCompleted(object sender, MobileWebRepository.GetAvailableCountersForDeviceCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}