using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSideMiniGraber.VKLogic;

namespace ServerSideMiniGraber.CommandLogic
{
    abstract class Command
    {
        protected VkLogic vk;
        public string Prefix { get; protected set; }

        public Command(string prefix, VkLogic vk)
        {
            this.Prefix = prefix;
            this.vk = vk;
        }

        public abstract Task<string> DoRequestAsync(string param);
    }
}
