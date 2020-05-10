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
            SendCommand = new Command(ConnectToServer);
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

        Byte[] data;
        TcpClient client;
        void ConnectToServer()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = Int32.Parse(Device.Port);
                client = new TcpClient(Device.IP, port);

                data = new Byte[5];

                data[0] = 0xA4;
                data[1] = 0x02;
                data[2] = 0x41;
                data[3] = 0x54;
                data[4] = 0x3B;

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[5];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Debug.WriteLine("Received: {0}", responseData);

                data = new Byte[4];

                data[0] = 0xA5;
                data[1] = 0x01;
                data[2] = (Byte)Int32.Parse(Device.Num_Of_Led);
                data[3] = (Byte)(data[0] + data[1] + data[2]);
                stream.Write(data, 0, data.Length);

                // String to store the response ASCII representation.
                responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Debug.WriteLine("Received: {0}", responseData);

                data = new Byte[6];

                data[0] = 0xA0;
                data[1] = 0x03;
                data[2] = (Byte)Red;
                data[3] = (Byte)Green;
                data[4] = (Byte)Blue;
                data[5] = (Byte)(data[0] + data[1] + data[2] + data[3] + data[4] + data[5]);

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
                // String to store the response ASCII representation.
                responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Debug.WriteLine("Received: {0}", responseData);

                //Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
