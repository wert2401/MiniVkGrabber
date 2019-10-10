using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSideMiniGraber.CommandLogic
{
    interface ICommand
    {
        public string Prefix { get; set; }
        public Task<string> DoRequestAsync(string param);
    }
}
