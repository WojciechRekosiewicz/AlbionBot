using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AlbionBot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        private static SocketTextChannel channel;
        private static LinkedList<string> guildMembers;
        private static LinkedList<string> discordMembers;





        internal static Task StartTimer()
        {
            // gets server id and channel id
            //channel = Global.Client.GetGuild(621807985349361695).GetTextChannel(621807985349361697);
            channel = Global.Client.GetGuild(541341695074107392).GetTextChannel(631871642917928971);

            loopingTimer = new Timer()
            {
                Interval = 20000,
                AutoReset = true,
                Enabled = true
            };
            //loopingTimer.Elapsed += OnTimerTicked;
            loopingTimer.Elapsed += CheckGuild;

            return Task.CompletedTask;

        }

        private static async void GatherDiscordUsers(object sender, ElapsedEventArgs e)
        {
            for (int index = 0; index < discordMembers.Count(); index++)
            {

            }
        }



        private static async void CheckGuild(object sender, ElapsedEventArgs e)
        {
            //LinkedList<string> guildMembers = new LinkedList<string>();
            //LinkedList<string> discordMembers = new LinkedList<string>();

            if (Global.Client == null)
            {
                Console.WriteLine("tick before client rdy");       
            }

            //for (int index = 0; index < discordMembers.Count(); index++)
            //{
               
            //}

            //string nickname = "";
            //string json = "";
            //using (WebClient client = new WebClient())
            //{
            //    json = client.DownloadString($"https://gameinfo.albiononline.com/api/gameinfo/search?q={nickname}");
            //}

            //var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            //var name = dataObject.players[0].Name.ToString();
            //var guild = dataObject.players[0].GuildName.ToString();

            

  


         

            await channel.SendMessageAsync("$x");

            //  await Context.Channel.SendMessageAsync($"Nick : {name} guild : {guild}");



            //var serverNickname = (user as IGuildUser).Nickname;

            //var asd = Context.User;
            //Console.WriteLine(serverNickname);

            //var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");


            //if (name == nickname && guild == "Nagelfar" && serverNickname == nickname)

            //{
            //    await (user as IGuildUser).AddRoleAsync(role);
            //    await Context.Channel.SendMessageAsync($"{name} you are member of Nagelfar. I will now register you.");
            //}
            //else if (serverNickname != nickname)
            //{
            //    await Context.Channel.SendMessageAsync($"{serverNickname} name you enter ({name}) is not you name on discord! Plz set your name on discord to the same as your name in game!");
            //}
            //else
            //{

            //    await Context.Channel.SendMessageAsync($"{name} you are not member of Nagelfar!");
            //}

        }


        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            if (Global.Client == null)
            {
                Console.WriteLine("tick before client rdy");
                return;
            }

            await channel.SendMessageAsync("ping");
        }
    }
}
