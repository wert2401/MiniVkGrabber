using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSideMiniGraber.CommandLogic.Commands;
using ServerSideMiniGraber.VKLogic;
using ServerSideMiniGraber.ServerLogic;

namespace ServerSideMiniGraber.CommandLogic
{
    class CommandHolder
    {
        List<ICommand> commands = new List<ICommand>();
        VkLogic vk = new VkLogic(FileManager.GetToken());
        public CommandHolder()
        {
            commands.Add(new CmdGetFriends("getFriends", vk));
        }

        public async Task<string> DoCommand(CommandObject message)
        {
            foreach (ICommand command in commands)
            {
                if (command.Prefix == message.Method)
                {
                    return await command.DoRequestAsync(message.Param);
                }
            }
            return "There is no such command";
        }
    }
}
