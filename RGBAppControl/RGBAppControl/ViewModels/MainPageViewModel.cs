using RGBAppControl.Models;
using RGBAppControl.Services;
using RGBAppControl.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RGBAppControl.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
            }
        }

        ObservableCollection<Models.Device> devices;
        
        public ObservableCollection<Models.Device> Devices
        {
            get
            {
                return devices;
            }
            set
            {
                devices = value;
                NotifyPropertyChanged();
            }
        }



        string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (message != value)
                {
                    message = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public MainPageViewModel()
        {
            Devices = new ObservableCollection<Models.Device>();
        }

        public MainPageViewModel(ObservableCollection<Models.Device> startingItems)
        {
            Devices = startingItems;
        }

        public ICommand EditCommand => new Command<Models.Device>(async (Models.Device item) =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditDevicePage(item));
            //await Application.Current.MainPage.Navigation.PushAsync(new EditDevicePage());
        });

        public ICommand ControlCommand => new Command<Models.Device>(async (Models.Device item) =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ControlDevicePage(item));
        });

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    devices = DataService.GetListOfDevices();
                    NotifyPropertyChanged();

                    IsRefreshing = false;
                });
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
