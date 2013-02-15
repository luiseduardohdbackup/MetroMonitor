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

                var grid = new Grid();
                grid.Children.Add(new Rectangle { Fill = colour });
                grid.Children.Add(new TextBlock
                {
                    Text = res.DeviceName + res.Status,
                    DataContext = res.Id,
                    
                });

                //listSource.Add(tb);
                g.Add(grid);
                // data.
            }
           // statuslistSelector.ItemsSource = g;
        }

        private void GenertateStatusList()
        {

           

        }

    
        //void dataClient_GetAvailableCountersForDeviceCompleted(object sender, MobileWebRepository.GetAvailableCountersForDeviceCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}