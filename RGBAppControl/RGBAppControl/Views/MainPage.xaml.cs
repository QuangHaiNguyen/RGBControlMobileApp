using RGBAppControl.Services;
using RGBAppControl.ViewModels;
using RGBAppControl.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RGBAppControl
{
    public partial class MainPage : ContentPage
    {
        string DbPath => GetLocalFilePath("people.db3");
        MainPageViewModel mainPageViewModel;

        public MainPage()
        {
            DataService.DeviceDatabase(DbPath);

            InitializeComponent();
            mainPageViewModel = new MainPageViewModel(DataService.GetListOfDevices());
            BindingContext = mainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = null;
            mainPageViewModel = new MainPageViewModel(DataService.GetListOfDevices());
            BindingContext = mainPageViewModel;

        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddDevicePage()));
        }

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditDevicePage()));
        }

        public string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
