using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGraber.ClientLogic
{
    class CommandObject
    {
        public string Method;
        public string Param;

        public CommandObject(string method, string param)
        {
            Method = method;
            Param = param;
        }

        public string GetString()
        {
            return Method + ":" + Param;
        }
    }
}
