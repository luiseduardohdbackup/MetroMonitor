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
    public partial class HomePage : PhoneApplicationPage
    {

        MobileWebRepository.DataRepositoryClient dataClient = new MobileWebRepository.DataRepositoryClient();
        
        public HomePage()
        {
            InitializeComponent();
        }

        private void GenerateData() {

            //var data = dataClient.GetAvailableDevicesAsync();

        
        }
    }
}