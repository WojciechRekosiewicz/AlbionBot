using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AlbionBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("Echo")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync($"Not you are {message}");  
        }
    }
}
