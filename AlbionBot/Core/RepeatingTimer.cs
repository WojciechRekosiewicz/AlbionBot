using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
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
            channel = Global.Client.GetGuild(621807985349361695).GetTextChannel(621807985349361697);

            loopingTimer = new Timer()
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;         

            return Task.CompletedTask;

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
