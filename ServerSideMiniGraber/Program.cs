using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerSideMiniGraber.ServerLogic;

namespace ServerSideMiniGraber
{
    class Program
    {
        static Server server;
        static void Main(string[] args)
        {
            server = new Server("127.0.0.1", 8888);
            Console.WriteLine("Start to listen");
            Thread serverThread = new Thread(new ThreadStart(server.StartListen));
            serverThread.Start();
            Console.WriteLine("Press any button to close the server...");
            Console.ReadKey();
            server.Close();
            serverThread.Abort();
        }
    }
}
