using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

/// <summary>
//
/// Lighting Api
//
/// <iot:Capability Name="lowLevelDevices" />
/// <DeviceCapability Name = "109b86ad-f53d-4b76-aa5f-821e2ddf2141" />
/// </summary>
namespace DemoTM1638
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TM1638 m_t;
        public MainPage()
        {
            this.InitializeComponent();
            setup();
        }

        private async void setup()
        {
            //if (LightningProvider.IsLightningEnabled)
            //{
            //    LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
            //}

            m_t = new TM1638(19, 13, 6);
            await m_t.WaitForInitialiation();

        }
    }
}
