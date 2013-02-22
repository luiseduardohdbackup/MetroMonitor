using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MetroMonitor.DesktopInterface.MetroMonitorWebRepository;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MetroMonitor.DesktopInterface
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddCounterView : MetroMonitor.DesktopInterface.Common.LayoutAwarePage
    {
        MetroMonitorWebRepository.DataRepositoryClient dataClient = new MetroMonitorWebRepository.DataRepositoryClient();

        MetroMonitorWebRepository.DeviceContractsClient deviceClient = new MetroMonitorWebRepository.DeviceContractsClient();

        MetroMonitorWebRepository.CounterContractsClient counterClient = new MetroMonitorWebRepository.CounterContractsClient();

        public AddCounterView()
        {
          
            this.InitializeComponent();
          //  CounterToAddDD.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            LoadDeviceDropDownContent();
        }

        private async void LoadDeviceDropDownContent() { 

            var deviceData = await dataClient.GetAvailableDevicesAsync();

            var listData = new List<TextBlock>();

            foreach(var r in deviceData){

                listData.Add(new TextBlock{Text = r.Value.ToString(), DataContext = r.Key});

                //DeviceNameDD.Items.Add(new ComboBoxItem
                //{
                //    Content = r.Value,
                //    Name = r.Key.ToString(),
                //    DataContext = r.Key
                    
                //});

            }deviceListView.ItemsSource = listData;

        
        }

        //private void GenerateReadIntervalUI() {
        //    SetPropsRec.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    RITB.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    for (int i = 0; i < 20; i++)
        //    {
        //        ReadIntervalDD.Items.Add(new ComboBoxItem
        //        {
        //            DataContext = i + 4,
        //            Name = i + 4.ToString(),
        //            Content = i + 4

        //        });
        //    }

        //    ReadIntervalDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

        //}

        //private void GenerateLogIntervalUI()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        LogIntervalDD.Items.Add(new ComboBoxItem
        //        {
        //            DataContext = i + 4,
        //            Name = i + 4.ToString(),
        //            Content = i + 4

        //        });
        //    }

        //    LogIntervalDD.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    LITB.Visibility = Windows.UI.Xaml.Visibility.Visible;

        //}

        //private void GenerateMaxThresholUI()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        MaxThresholdDD.Items.Add(new ComboBoxItem
        //        {
        //            DataContext = i + 4,
        //            Name = i + 4.ToString(),
        //            Content = i + 4

        //        });
        //    }

        //    MaxThresholdDD.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    MTTB.Visibility = Windows.UI.Xaml.Visibility.Visible;

        //}

        private void GenerateComboBoxContent(ComboBox dropDown, TextBlock textblock)
        {


            
            for (int i = 0; i < 20; i++)
            {
                dropDown.Items.Add(new ComboBoxItem
                {
                    DataContext = i + 4,
                    Name = i + 4.ToString(),
                    Content = i + 4

                });
            }

            if (dropDown.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                dropDown.Visibility = Windows.UI.Xaml.Visibility.Visible;
                textblock.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }



        }

        //private void GenerateMinThresholdUI()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        MinThresholdDD.Items.Add(new ComboBoxItem
        //        {
        //            DataContext = i + 4,
        //            Name = i + 4.ToString(),
        //            Content = i + 4

        //        });
        //    }
        //    MinThresTB.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    MinThresholdDD.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    SetPropsRec.Visibility = Windows.UI.Xaml.Visibility.Visible;

        //}

        private void GenerateConfirmationButtonUI() {
            ConfirmationButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

 

        private void SerializeFormData() {

            var d = deviceListView.SelectedItem;
            var e = (TextBlock)d;

            var c = counterListView.SelectedItem;
            var sc = (TextBlock)c;

            var ri = ReadIntervalDD.SelectedItem;
            var ricb = (ComboBoxItem)ri;

            var li = LogIntervalDD.SelectedItem;
            var licb = (ComboBoxItem)li;

            var maxt = MaxThresholdDD.SelectedItem;
            var maxtcb = (ComboBoxItem)maxt;

            var mint = MinThresholdDD.SelectedItem;
            var mintcb = (ComboBoxItem)mint;

            AddedCounterNotificationTB.Text = sc.Text.ToString() + " Successfully Added to " + e.Text.ToString();

            counterClient.AddMetricAsync(new CounterCreate
            {
                DeviceName = e.Text.ToString(),
                DeviceId = (int)e.DataContext,
                CounterDefinitifionId = (int)sc.DataContext,

                Metric = new CounterBase {
                Description = string.Empty,
                LogInterval = (int)licb.Content,
                MaxThreshold = (int)maxtcb.Content,
                MinThreshold = (int)mintcb.Content,
                ReadInterval = (int)ricb.Content
                }
            });
            
        }

        private async void getCounterList() {

          //  CounterToAddDD.Visibility = Windows.UI.Xaml.Visibility.Visible;

            var r = await counterClient.LoadAvailableCountersAsync();
            var dataList = new List<TextBlock>();

            foreach (var result in r)
            {
                dataList.Add(new TextBlock{Text = result.Value.ToString(), DataContext = result.Key});

                //CounterToAddDD.Items.Add(new ComboBoxItem
                //{
                //    DataContext = result.Key,
                //    Name = result.Key.ToString(),                    
                //    Content = result.Value

                //});
            }counterListView.ItemsSource = dataList;

        }

        
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        
               
        private void DeviceNameDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getCounterList();
        }

      
        private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            SerializeFormData();
        }

        private void deviceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getCounterList();

        }

        private void counterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SetPropsRec.Visibility != Windows.UI.Xaml.Visibility.Visible) { SetPropsRec.Visibility = Windows.UI.Xaml.Visibility.Visible; }

            GenerateComboBoxContent(ReadIntervalDD, RITB);

            GenerateComboBoxContent(LogIntervalDD, LITB);

            GenerateComboBoxContent(MaxThresholdDD, MTTB);

            GenerateComboBoxContent(MinThresholdDD, MinThresTB);

            GenerateConfirmationButtonUI();

        }
    }
}
