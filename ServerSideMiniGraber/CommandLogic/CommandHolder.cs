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
        List<Command> commands = new List<Command>();
        VkLogic vk = new VkLogic(FileManager.GetToken());
        public CommandHolder()
        {
            commands.Add(new CmdGetFriends("getFriends", vk));
            commands.Add(new CmdGetPerson("getPerson", vk));
        }

        public async Task<string> DoCommand(CommandObject message)
        {
            foreach (Command command in commands)
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
