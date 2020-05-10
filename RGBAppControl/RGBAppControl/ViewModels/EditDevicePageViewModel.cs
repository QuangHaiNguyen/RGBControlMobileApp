using RGBAppControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RGBAppControl.ViewModels
{
    public class EditDevicePageViewModel : INotifyPropertyChanged
    {

        Models.Device device;
        public Models.Device Device
        {
            get
            {
                return device;
            }
            set
            {
                device = value;
                NotifyPropertyChanged();
            }
        }
        public EditDevicePageViewModel(Models.Device selectedDevice)
        {
            Device = selectedDevice;
            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            UpdateCommand = new Command(async () =>
            {
                
                //Update is delete and add new
                Services.DataService.AddDevice(Device);
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            DeleteCommand = new Command(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Do you want to delete the device", "Yes", "No");
                if (!answer)
                    return;
                Services.DataService.DeleteDevice(Device);
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CancelCommand { get; }
        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get;  }
    }
}
