using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGraber.ClientLogic;
using MiniGraber.Objects;
using MiniGraber.Utils;

namespace MiniGraber
{
    class Requests
    {
        Client client;
        public Requests(string ip, int port)
        {
            client = new Client();
            try
            {
                client.Connect(ip, port);
            }
            catch
            {
                throw;
            }
        }

        public List<Person> GetFriends(string id)
        {
            string resp = "";
            if (client.IsConnected)
            {
                try
                {
                    resp = client.SendRequest(new CommandObject("getFriends", id));
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
            if (client.IsConnected)
            {
                try
                {
                    resp = client.SendRequest(new CommandObject("getPerson", id));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return JSONProcessor.ParsePerson(resp);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }
    }
}
