using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSideMiniGraber.VKLogic;

namespace ServerSideMiniGraber.CommandLogic.Commands
{
    class CmdGetFriends : Command
    {
        public CmdGetFriends(string prefix, VkLogic vk)
        :base(prefix, vk)
        {

        }

        public override async Task<string> DoRequestAsync(string param)
        {
            int id = 0;
            if (!int.TryParse(param, out id))
            {
                string idString = await vk.GetPersonId(param);
                int.TryParse(idString, out id);
            }
            return await vk.GetPersonFriends(id.ToString());
        }
    }
}
