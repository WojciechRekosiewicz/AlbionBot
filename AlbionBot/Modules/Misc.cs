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
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());  
        }

        [Command("Rules")]
        public async Task Rules ()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Rules");
            embed.WithDescription(Utilities.GetAlert("TEST"));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Rekrutacja")]
        public async Task Recrutment()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Rules");
            embed.WithDescription(Utilities.GetAlert("Rules"));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
