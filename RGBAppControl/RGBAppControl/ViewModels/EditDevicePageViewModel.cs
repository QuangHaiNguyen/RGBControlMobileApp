using RGBAppControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RGBAppControl.ViewModels
{
    public class EditDevicePageViewModel : INotifyPropertyChanged
    {

        Device device;
        public Device Device
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
        public EditDevicePageViewModel(Device selectedDevice)
        {
            Device = selectedDevice;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
