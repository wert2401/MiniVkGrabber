using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace MiniGraber.ClientLogic
{
    class Client
    {
        TcpClient client;
        NetworkStream stream;
        public bool IsConnected { 
            get 
            {
                return client.Connected;
            } 

            private set { }
        }

        public Client()
        {
            client = new TcpClient();
        }

        public void Connect(string ip, int port)
        {
            try
            {
                client.Connect(IPAddress.Parse(ip), port);
                stream = client.GetStream();
                SendRequest(new CommandObject("Test", "Test"));
            }
            catch
            {
                throw;
            }
        }

        private string GetResponse()
        {
            try
            {
                byte[] data = new byte[64];
                StringBuilder builder = new StringBuilder();
                do
                {
                    stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data));
                } while (stream.DataAvailable);

                return builder.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string SendRequest(CommandObject command)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(command.GetString());
                stream.Write(data, 0, data.Length);
                string response = GetResponse();
                return response;
            }
            catch (Exception)
            {
                client.Close();
                throw;
            }
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
