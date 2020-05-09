using RGBAppControl.Models;
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

        public ICommand EditCommand => new Command<Models.Device>((Models.Device item) =>
        {
            Message = $"Edit command was called on: {item.Name}";
            Application.Current.MainPage.Navigation.PushAsync(new EditDevicePage(item));
        });

        public ICommand DeleteCommand => new Command<Models.Device>((Models.Device item) =>
        {
            Message = $"Delete command was called on: {item.Name}";
        });
        public ICommand ControlCommand => new Command<string>((string item) =>
        {
            Message = $"Control command was called on: {item}";
        });

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
