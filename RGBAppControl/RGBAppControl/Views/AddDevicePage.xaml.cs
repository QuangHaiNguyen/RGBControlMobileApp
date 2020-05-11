using RGBAppControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBAppControl.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RGBAppControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDevicePage : ContentPage
    {
        public Models.Device Device { get; set; }
        public AddDevicePage()
        {
            InitializeComponent();
            Device = new Models.Device
            {
                Name = "Device Name",
                IP = "Device IP",
                Port = "Device Port",
                Num_Of_Led = "0",
            };

            BindingContext = this;
            
        }

        async void ToolbarItemCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void ToolbarItemSave_Clicked(object sender, EventArgs e)
        {
            DataService.AddDevice(Device);
            await Navigation.PopModalAsync();
            await Application.Current.MainPage.DisplayAlert("Alert", "New device is created", "OK");
        }
    }
}