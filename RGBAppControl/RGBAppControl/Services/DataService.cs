using RGBAppControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RGBAppControl.Services
{
    public static class DataService
    {
        static ObservableCollection<Device> devices;

        public static ObservableCollection<Device> GetListItems()
        {
            

            devices = new ObservableCollection<Device>()
            {
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 1", IP = "192.168.0.38", Port = "13000", Num_Of_Led = "10"},
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 2", IP = "192.168.0.2", Port = "13000", Num_Of_Led = "10" },
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 3", IP = "192.168.0.3", Port = "13000", Num_Of_Led = "10" },
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 4", IP = "192.168.0.4", Port = "13000", Num_Of_Led = "10" },
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 5", IP = "192.168.0.5", Port = "13000", Num_Of_Led = "10" },
                new Device { Id = Guid.NewGuid().ToString(), Name = "Device 6", IP = "192.168.0.6", Port = "13000", Num_Of_Led = "10" },
            };

            return devices;
        }

        public static void AddDevice(Device device) 
        {
            devices.Add(device);
        }
    }
}
