using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RGBAppControl.Services
{
    public static class TCPClientService
    {
        static TcpClient client;
        static NetworkStream stream;
        static Byte[] data;
        public static void ClientConnect(string Ip, int Port, int numOfLED)
        {
            try
            {
                client = new TcpClient(Ip, Port);
                stream = client.GetStream();
            }
            catch
            {
                throw new Exception("Cannot connect to server!");
            }

            data = new Byte[5];

            data[0] = 0xA4;
            data[1] = 0x02;
            data[2] = 0x41;
            data[3] = 0x54;
            data[4] = 0x3B;

            try
            {
                stream.Write(data, 0, data.Length);

                data = new Byte[5];
                stream.Read(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            if (VerifyOKReply(data, data.Length) != true)
            {
                throw new Exception("No reply from server");
            }

            data = new Byte[4];
            data[0] = 0xA5;
            data[1] = 0x01;
            data[2] = (Byte)numOfLED;
            data[3] = (Byte)(data[0] + data[1] + data[2]);
            
            try
            {
                stream.Write(data, 0, data.Length);

                data = new Byte[5];
                stream.Read(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (VerifyOKReply(data, data.Length) != true)
            {
                throw new Exception("Configure Error!");
            }
        }

        public static void ClientSendColor(int r, int g, int b)
        {
            data = new Byte[6];

            data[0] = 0xA0;
            data[1] = 0x03;
            data[2] = (Byte)r;
            data[3] = (Byte)g;
            data[4] = (Byte)b;
            data[5] = (Byte)(data[0] + data[1] + data[2] + data[3] + data[4] + data[5]);

            try
            {
                stream.Write(data, 0, data.Length);
                data = new Byte[5];
                stream.Read(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (VerifyOKReply(data, data.Length) != true)
            {
                throw new Exception("Send Data Error!");
            }
        }

        public static void ClientDisconnect()
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool VerifyOKReply( Byte[] data, int length)
        {
            if (data[0] != 0xA7 || data[2] != 0x4F || data[3] != 0x4B)
            {
                return false;
            }
            return true;
        }
    }

    
}
