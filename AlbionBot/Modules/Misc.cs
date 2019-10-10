using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbionBot.Core.UserAccounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NReco.ImageGenerator;
using System.Net;
using Newtonsoft.Json;

namespace AlbionBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        [Command("Person")]
        public async Task GetPerson()
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://gameinfo.albiononline.com/api/gameinfo/players/gV5ro9GCTq-mF-yr-WierA");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            //var name = dataObject.results[3].AverageItemPower.ToString();
            var name = dataObject.Name.ToString();
            await Context.Channel.SendMessageAsync($"Nick : {name}");

        }


        [Command("Register")]
        public async Task Register([Remainder]string nickname)
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString($"https://gameinfo.albiononline.com/api/gameinfo/search?q={nickname}");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            var name = dataObject.players[0].Name.ToString();
            var guild = dataObject.players[0].GuildName.ToString();
            //  await Context.Channel.SendMessageAsync($"Nick : {name} guild : {guild}");

            
            var user = Context.User;
            
            var getId = Context.User.Username;
            //var use = guild.GetUser(getId);

            var serverNickname = (user as IGuildUser).Nickname;

            var asd = Context.User;        
            Console.WriteLine(serverNickname);


           

            

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");
            

            if (name == nickname && guild == "Nagelfar" && serverNickname == nickname)
                //if (name == nickname && guild == "Nagelfar" && user.ToString() == nickname)
            {
                await (user as IGuildUser).AddRoleAsync(role);
                await Context.Channel.SendMessageAsync($"{name} you are member of Nagelfar. I will now register you.");
            }
            else if (serverNickname != nickname)
            {
                await Context.Channel.SendMessageAsync($"{serverNickname} name you enter ({name}) is not you name on discord! Plz set your name on discord to the same as your name in game!");
            }
            else
            {
                //  await Context.Channel.SendMessageAsync($"test");
                await Context.Channel.SendMessageAsync($"{name} you are not member of Nagelfar!");
            }

        }

        [Command("Greeting")]
        public async Task Test()
        {
            string css = "<style>\n h1{\n color: red;\n }\n</style> \n ";
            string html = String.Format("<h1> Hi {0}</h1>", Context.User.Username);
            var converter = new HtmlToImageConverter
            {
                Width = 250,
                Height = 70
            };
            var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
            await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), "User.jpg");

        }

        [Command("stats")]
        public async Task MyXP()
        {
            UserAccount account = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendMessageAsync($" EX : {account.XP} and Points : {account.Points}");
        }


        [Command("Echo")]
        public async Task Echo([Remainder]string message)
        {           
            var embed = new EmbedBuilder();
            embed.WithTitle($"Echoed message {Context.User.Username}");
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

        [Command("Data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync($"Data has {DataStorage.GetPairsCount()} elements.");
            DataStorage.AddPairToStorage("Nickname", "LeeGe");
        }
    }
}
