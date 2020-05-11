using RGBAppControl.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;

namespace RGBAppControl.ViewModels
{
    class ControlDevicePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command SendCommand { get; }
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Device)));
            }
        }
        public ControlDevicePageViewModel(Models.Device selectedDevice)
        {
            Device = selectedDevice;
            //SendCommand = new Command(ConnectToServer);
            SendCommand = new Command(async () =>
            {
                //ConnectToServer();
                try
                {
                    TCPClientService.ClientConnect(Device.IP, Int32.Parse(Device.Port), Int32.Parse(Device.Num_Of_Led));
                    TCPClientService.ClientSendColor(Red, Green, Blue);
                    TCPClientService.ClientDisconnect();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                
            });
        }

        int red;
        public int Red
        {
            get { return red; }
            set
            {
                red = value;
                Color = Color.FromRgb(value, green, blue);
            }
        }
        
        int green = 255;
        public int Green
        {
            get { return green; }
            set
            {
                green = value;
                Color = Color.FromRgb(red, value, blue);
            }
        }

        int blue;
        public int Blue
        {
            get { return blue; }
            set
            {
                blue = value;
                Color = Color.FromRgb(red, green, value);
            }
        }

        Color color;
        public Color Color
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Red"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Green"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Blue"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                }
            }
            get
            {
                return color;
            }
        }
    }
}
