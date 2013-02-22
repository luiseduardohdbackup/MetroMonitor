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
    public partial class Charts : PhoneApplicationPage
    {
        private string deviceID;
        private string counterID;
        MobileDataRepo.GraphContractClient graphClient = new MobileDataRepo.GraphContractClient();  
        public Charts()
        {
            InitializeComponent();
           // this.MyLineSeriesChart.DataContext = new Point[] { new Point(0, 2), new Point(1, 10), new Point(2, 6) };
        }

        private void GenerateChart() {

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("Text"))
            {
                string[] prams = parameters["Text"].Split(' ');
                counterID = prams[0];
                deviceID = prams[1];

                graphClient.MetricsOverveiwForGraphForCounterCompleted += graphClient_MetricsOverveiwForGraphForCounterCompleted;
                graphClient.MetricsOverveiwForGraphForCounterAsync(Convert.ToInt32(deviceID), Convert.ToInt32(counterID));
                //GenerateCounterCounterDropDown(parameters["Text"]);
            }
        }

        void graphClient_MetricsOverveiwForGraphForCounterCompleted(object sender, MobileDataRepo.MetricsOverveiwForGraphForCounterCompletedEventArgs e)
        {
            //var graphPoint = new Point[];
            var f = e.Result;
            foreach(var d in e.Result.PlottingData){
            var graphPoint = new Point[d.Value.Count];

                for(int i = 0; i <= d.Value.Count -1; i++){
                graphPoint[i] = new Point{
                        X = d.Value.ElementAt(i).LogDate.ToOADate(),
                        Y = d.Value.ElementAt(i).AverageRead / 100000
                    };
                
                }
             
                   this.MyLineSeriesChart.DataContext = graphPoint;
                       //new Point[] { new Point((double)i.AverageRead, (double)i.LogDate.ToOADate)), new Point(1, 10), new Point(2, 6)
                }
          
            
          
        }
    }
}