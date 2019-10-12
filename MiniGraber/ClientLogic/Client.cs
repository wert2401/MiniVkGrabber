using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using MiniGraber.Objects;
using MiniGraber.Utils;

namespace MiniGraber.ClientLogic
{
    class Client
    {
        TcpClient client;
        NetworkStream stream;

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
                byte[] data = new byte[256];
                StringBuilder builder = new StringBuilder();
                do
                {
                    Array.Clear(data, 0, data.Length);
                    stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data));
                } while (stream.DataAvailable);

                return builder.ToString().Replace("\0", "");
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private string SendRequest(CommandObject command)
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

        public List<Person> GetFriends(string id)
        {
            string resp = "";
            if (client.Connected)
            {
                try
                {
                    resp = SendRequest(new CommandObject("getFriends", id));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return JSONProcessor.ParsePeople(resp);
        }

        public Person GetPerson(string id)
        {
            string resp = "";
            if (client.Connected)
            {
                try
                {
                    resp = SendRequest(new CommandObject("getPerson", id));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return  JSONProcessor.ParsePerson(resp);
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
