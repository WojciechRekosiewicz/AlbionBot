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
            channel = Global.Client.GetGuild(541341695074107392).GetTextChannel(631871642917928971);

            loopingTimer = new Timer()
            {
                Interval = 30000000,
                AutoReset = true,
                Enabled = true
            };
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
            await channel.SendMessageAsync("$x");




























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
