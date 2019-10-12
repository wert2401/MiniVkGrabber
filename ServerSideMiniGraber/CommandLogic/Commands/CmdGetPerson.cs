using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSideMiniGraber.VKLogic;

namespace ServerSideMiniGraber.CommandLogic.Commands
{
    class CmdGetPerson : Command
    {
        public CmdGetPerson(string prefix, VkLogic vk)
        : base(prefix, vk)
        { }

        public override async Task<string> DoRequestAsync(string param)
        {
            string resp = await vk.GetPerson(param);
            return resp;
        }
    }
}
