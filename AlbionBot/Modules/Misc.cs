using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

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

        [Command("Secret")]
        public async Task Secret([Remainder]string arg = "")
        {
            if (!UserIsNagelfar((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You don't have required role to do that");
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsNagelfar(SocketGuildUser user)
        {
            //user.Guild.Roles 
            string targetRoleName = "Nagelfar";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;

            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }

        [Command("Blame")]
        public async Task Blame([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Shame!!");
            embed.WithDescription(Utilities.GetFormattedAlert("BLAME", message));
            embed.WithColor(255, 0, 0);
            embed.WithThumbnailUrl("https://www.caycon.com/wp-content/uploads/2014/11/Dont-Start-a-Business-if-You-Have-a-Victim-Mentality.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random ran = new Random();
            string selection = options[ran.Next(0, options.Length)];



            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));
            embed.WithThumbnailUrl("https://timesofindia.indiatimes.com/thumb/msid-67586673,width-800,height-600,resizemode-4/67586673.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Rules")]
        public async Task Rules ()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Rules");
            embed.WithDescription(Utilities.GetAlert("TEST"));
            embed.WithColor(new Color(0, 255, 0));
            embed.WithThumbnailUrl("https://media.istockphoto.com/vectors/crown-flat-design-fantasy-icon-vector-id965301668?k=6&m=965301668&s=612x612&w=0&h=WQW6DRiWp8d-8jm7yosonQbctnWMwb0Bb_1fFriIuOs=");


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
