using RGBAppControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.Diagnostics;

namespace RGBAppControl.Services
{
    
    public class DataService
    {
        static SQLiteConnection conn;

        public static void DeviceDatabase(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Device>();
        }

        public static ObservableCollection<Device> GetListOfDevices()
        {
            try
            {
                return new ObservableCollection<Device>(conn.Table<Device>().ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            return new ObservableCollection<Device>();
        }

        public static void AddDevice(Device device) 
        {
            try
            {
                //basic validation to ensure a name was entered
                if (device.Id != 0)
                {
                    conn.Update(device); ;
                }
                else
                {
                    if (string.IsNullOrEmpty(device.Name))
                        throw new Exception("Valid name required");
                    conn.Insert(device);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        public static void DeleteDevice(Device device)
        {
            try
            {
                conn.Delete(device);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
