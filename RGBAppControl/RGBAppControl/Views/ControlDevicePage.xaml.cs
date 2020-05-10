using RGBAppControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RGBAppControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlDevicePage : ContentPage
    {
        public ControlDevicePage()
        {
            InitializeComponent();
        }

        public ControlDevicePage(Models.Device selectedDevice)
        {
            InitializeComponent();
            BindingContext = new ControlDevicePageViewModel(selectedDevice);
        }
    }
}