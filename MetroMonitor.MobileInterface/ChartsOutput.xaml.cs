using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MetroMonitor.MobileInterface
{
    public partial class ChartsOutput : PhoneApplicationPage
    {
        MobileDataRepo.DataRepositoryClient dataClient = new MobileDataRepo.DataRepositoryClient();
        MobileDataRepo.GraphContractClient graphDataClient = new MobileDataRepo.GraphContractClient();
        
        public ChartsOutput()
        {
            InitializeComponent();
            graphDataClient.MetricsOverveiwForDeviceCompleted += graphDataClient_MetricsOverveiwForDeviceCompleted;
            graphDataClient.MetricsOverveiwForDeviceAsync(1);

           // this.MyLineSeriesChart.DataContext = new Point[] { new Point(0, 2), new Point(1, 10), new Point(2, 6) };
        }

        void graphDataClient_MetricsOverveiwForDeviceCompleted(object sender, MobileDataRepo.MetricsOverveiwForDeviceCompletedEventArgs e)
        {
            var datapoints = new Point[e.Result.PlottingData.ElementAt(0).Value.Count];

     //       for (int i = 0; i <= e.Result.PlottingData.ElementAt(0).Value.Count -1; i++)
            for (int i = 0; i <= 3; i++)
            {

                datapoints[i] = new Point(e.Result.PlottingData.ElementAt(0).Value.ElementAt(i).AverageRead, e.Result.PlottingData.ElementAt(0).Value.ElementAt(i).LogDate.ToOADate());
            }

           

            this.MyLineSeriesChart.DataContext = datapoints;
        }
    }
}