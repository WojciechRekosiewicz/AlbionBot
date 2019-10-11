﻿using Discord.WebSocket;
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



        internal static Task StartTimer()
        {
            // gets server id and channel id
            channel = Global.Client.GetGuild(621807985349361695).GetTextChannel(621807985349361697);

            loopingTimer = new Timer()
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            //loopingTimer.Elapsed += OnTimerTicked;
            loopingTimer.Elapsed += CheckGuild;

            return Task.CompletedTask;

        }

        private static async void CheckGuild(object sender, ElapsedEventArgs e)
        {
            if (Global.Client == null)
            {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://gameinfo.albiononline.com/api/gameinfo/players/gV5ro9GCTq-mF-yr-WierA");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            //var name = dataObject.results[3].AverageItemPower.ToString();
            var name = dataObject.Name.ToString();
            Console.WriteLine("checked");
            }
            //await Context.Channel.SendMessageAsync($"Nick : {name}");

        }


        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            if (Global.Client == null)
            {
                Console.WriteLine("tick before client rdy");
                return;
            }

           // await channel.SendMessageAsync("ping");
        }
    }
}