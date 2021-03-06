﻿using RGBAppControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RGBAppControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDevicePage : ContentPage
    {
        public EditDevicePage()
        {
            InitializeComponent();
        }



        public EditDevicePage(Models.Device selectedDevice)
        {
            InitializeComponent();
            BindingContext = new EditDevicePageViewModel(selectedDevice);
            //BindingContext = this;
            //Device = selectedDevice;
        }

        async void ToolbarItemUpdate_Clicked(object sender, EventArgs e)
        {
            //Services.DataService.AddDevice(Device);
            await Navigation.PopModalAsync();
        }

        async void ToolbarItemCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}