using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerSideMiniGraber.CommandLogic;

namespace ServerSideMiniGraber.ServerLogic
{
    class Connection
    {
        TcpClient client;
        NetworkStream stream;
        Server server;
        CommandHolder cmdHolder;

        public Connection(TcpClient client, Server server, CommandHolder cmdHolder)
        {
            this.client = client;
            stream = client.GetStream();
            this.server = server;
            this.cmdHolder = cmdHolder;
        }

        public async void ProcessAsync()
        {
            try
            {
                string fisrtMessage = GetMessage();
                if (fisrtMessage != "Test:Test")
                {
                    Disconnect();
                    return;
                }
                SendMessage("Hi");
                while (true)
                {
                    string request = GetMessage();
                    if (request != "")
                    {
                        Console.WriteLine("New request: " + request);
                    }

                    CommandObject command = new CommandObject(request);
                    string response = await cmdHolder.DoCommand(command);
                    SendMessage(response);
                }
            }
            catch 
            {
                Disconnect();
                return;
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));

            } while (stream.DataAvailable);

            return builder.ToString();
        }

        public void SendMessage(string data)
        {
            byte[] dataB = Encoding.UTF8.GetBytes(data);
            stream.Write(dataB, 0, dataB.Length);
        }

        public void Close()
        {
            client.Close();
        }

        public void Disconnect()
        {
            client.Close();
            server.RemoveConnection(this);
        }
    }
}
