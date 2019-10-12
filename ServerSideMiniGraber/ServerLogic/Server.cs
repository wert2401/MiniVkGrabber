using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerSideMiniGraber.CommandLogic;

namespace ServerSideMiniGraber.ServerLogic
{
    class Server
    {
        List<Connection> connections = new List<Connection>();
        List<Thread> threads = new List<Thread>();
        int port;
        IPAddress ip;
        TcpListener tcpListener;
        CommandHolder commandHolder;


        public Server(string ipAddress, int port)
        {
            this.port = port;
            ip = IPAddress.Parse(ipAddress);
            commandHolder = new CommandHolder();
        }

        public void StartListen()
        {
            tcpListener = new TcpListener(ip, port);
            tcpListener.Start();
            try
            {
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Connection cl = new Connection(client, this, commandHolder);
                    Thread clientThread = new Thread(new ThreadStart(cl.Process));
                    threads.Add(clientThread);
                    connections.Add(cl);
                    clientThread.Start();
                    Console.WriteLine("One connection was added. Connections now: " + connections.Count + ". Threads now: " + threads.Count);
                }
            }
            catch (SocketException e) when (e.ErrorCode == 10004)
            {
                return;
            }
        }

        public void Close()
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Close();
                threads[i].Abort();
            }
            tcpListener.Stop();
        }

        public void RemoveConnection(Connection connection)
        {
            int index = connections.IndexOf(connection);
            connections.Remove(connection);
            Thread t = threads[index];
            threads.Remove(threads[index]);
            Console.WriteLine("One connection was lost. Connections now: " + connections.Count + ". Threads now: " + threads.Count);
            t.Abort();
        }
    }
}
