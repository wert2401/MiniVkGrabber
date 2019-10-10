using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSideMiniGraber.VKLogic;

namespace ServerSideMiniGraber.CommandLogic.Commands
{
    class CmdGetFriends : ICommand
    {
        private VkLogic vk;
        public CmdGetFriends(string prefix, VkLogic vk)
        {
            this.Prefix = prefix;
            this.vk = vk;
        }
        public string Prefix { get; set; }

        public async Task<string> DoRequestAsync(string param)
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
