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
    public partial class CounterStatus : PhoneApplicationPage
    {
        MobileDataRepo.StatisticsContractClient statisticsClient = new MobileDataRepo.StatisticsContractClient();   
        

        public CounterStatus()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("Text"))
            {
                GenerateCounterCounterStatusesDropDown(Convert.ToInt32(parameters["Text"]));
            }
        }

        private void GenerateCounterCounterStatusesDropDown(int deviceId) {
            statisticsClient.GetCounterSummaryStatusAsync(deviceId);
            statisticsClient.GetCounterSummaryStatusCompleted += statisticsClient_GetCounterSummaryStatusCompleted;
        }

        void statisticsClient_GetCounterSummaryStatusCompleted(object sender, MobileDataRepo.GetCounterSummaryStatusCompletedEventArgs e)
        {
            var statuslist = new List<ListBoxItem>();
            foreach (var data in e.Result.Statistics)
            {

                var brush = new SolidColorBrush();

                brush.Color = Colors.Yellow;

                if (data.TimeFrameResult.ElementAt(0).Status.ToString().Equals("Red"))
                {
                    brush.Color = Colors.Red;
                }

                if (data.TimeFrameResult.ElementAt(0).Status.ToString().Equals("Green"))
                {
                    brush.Color = Colors.Green;
                }

                var lbi = new ListBoxItem
                {
                    Content = data.CounterName,
                    DataContext = data.TimeFrameResult.ElementAt(0).Trend.ToString(),
                    Background = brush,
                    
                };
                statuslist.Add(lbi);
            }


            CounterStatusListBox.ItemsSource = statuslist;
        }

        private void CounterStatusListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedItem = CounterStatusListBox.SelectedItem;
            var item = (ListBoxItem)selectedItem;
            
           
            MessageBox.Show("current Trend is " + item.DataContext.ToString());
        }
    }
}