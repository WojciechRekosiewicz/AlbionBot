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
using Newtonsoft.Json.Linq;

namespace AlbionBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
     

        [Command("Members")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Members()
        {           
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString($"https://gameinfo.albiononline.com/api/gameinfo/guilds/4BK_Vdp2R_ydqiy07asImg/members");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            int memCount = dataObject.Count;

            string[] nageMembers = new string[memCount];

            await Context.Channel.SendMessageAsync($"{memCount}");
          

            for(int i = 0; i < memCount; i++)
            {
                var name = dataObject[i].Name.ToString();

                nageMembers[i] = name;
            }

            foreach (string n in nageMembers)
            {
                await Context.Channel.SendMessageAsync($"{n}");              
            }
        }

        [Command("ClearRoles")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task ClearRoles()
        {
            var users = Context.Guild.Users;
            var userArray = users.ToArray();
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");

            for (int i = 0; i < users.Count; i++)
            {
                await userArray[i].RemoveRoleAsync(role);
            }
        }

        private async Task ResetRanks()
        {
            var users = Context.Guild.Users;
            var userArray = users.ToArray();
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");

            for (int i = 0; i < users.Count; i++)
            {
                await userArray[i].RemoveRoleAsync(role);
            }
        }

        [Command("Check")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task CheckRank()
        {            
            List<string> nagelfarMembers = GetNagelfarMembers();
            List<string> getNageDiscordMembers = GetNageDiscordMembers();

            var commonList = nagelfarMembers.Intersect(getNageDiscordMembers);
            var differences = getNageDiscordMembers.Except(nagelfarMembers);

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");

            var users = Context.Guild.Users;
            var userArray = users.ToArray();

            for (int i = 0; i < users.Count; i++)
            {
                var user = userArray[i];
                var getNickName = userArray[i].Nickname;
                if ((!commonList.Contains(user.Nickname)) && !(commonList.Contains(user.Username)) && ((differences.Contains(user.Username)) || (differences.Contains(user.Nickname))))
                {
                    if (getNickName == null)

                    {
                       // await Context.Channel.SendMessageAsync($"not nick {userArray[i].Username}");
                        await userArray[i].RemoveRoleAsync(role);
                    }
                    else
                    {
                      //  await Context.Channel.SendMessageAsync($"not user {userArray[i].Nickname}");
                        await userArray[i].RemoveRoleAsync(role);
                    }
                }
                else
                {
                    continue;  
                }
            }

            await Context.Channel.SendMessageAsync($"Done");
        }

        private List<string> GetNageDiscordMembers()
        {
            var users = Context.Guild.Users.ToArray();

            List<string> DiscordNageMembers = new List<string>();
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");
       //     SocketRole checkRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");

            for (int index = 0; index < users.Length; index++)
            {

                var user = (IGuildUser)users[index];
                var getNickName = users[index].Nickname;

                if (users[index].Guild.Roles.FirstOrDefault(y => y.Name == "Nagetest") != null)
                {

                    if (getNickName == null)
                    {
                        DiscordNageMembers.Insert(index, user.Username);
                    }
                    else
                    {
                        DiscordNageMembers.Insert(index, getNickName);
                    }
                }
            }

            return DiscordNageMembers;

        }


        private List<string> GetNagelfarMembers()
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString($"https://gameinfo.albiononline.com/api/gameinfo/guilds/4BK_Vdp2R_ydqiy07asImg/members");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            int memCount = dataObject.Count;

            List<string> nagelfarMembers = new List<string>();

            for (int i = 0; i < memCount; i++)
            {
                var name = dataObject[i].Name.ToString();
                nagelfarMembers.Add(name);
            }

            return nagelfarMembers;
        }

        private List<string> GetDiscordMembers()
        {
            var users = Context.Guild.Users.ToArray();

            List<string> DiscordMembers = new List<string>();

            for (int index = 0; index < users.Length; index++)
            {

                var user = (IGuildUser)users[index];
                var getNickName = users[index].Nickname;

                if (getNickName == null)
                {
                    DiscordMembers.Insert(index, user.Username);
                }
                else
                {
                    DiscordMembers.Insert(index, getNickName);
                }
            }

            return DiscordMembers;
          
        }

        [Command("Dmembers")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Traitement()
        {
            var users = Context.Guild.Users;
            var userArray = users.ToArray();                      

            for (int index = 0; index < users.Count; index++)
            {
                
                var user = (IGuildUser)userArray[index];
                var getNickName = userArray[index].Nickname;

                if(getNickName == null)
                {
                    await Context.Channel.SendMessageAsync(user.Username);
                }
                else
                {
                    await Context.Channel.SendMessageAsync(getNickName);
                }
            }
            await Context.Channel.SendMessageAsync($"{ users.Count}");
       
        }


        [Command("Person")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
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
            // var xs  = Context.Guild.GetUser(Context.User.Id).Nickname;   
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString($"https://gameinfo.albiononline.com/api/gameinfo/search?q={nickname}");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            var name = dataObject.players[0].Name.ToString();
            var guild = dataObject.players[0].GuildName.ToString();
            
            var user = Context.User;          

            var serverNickname = (user as IGuildUser).Nickname;

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");
            

            if (name == nickname && guild == "Nagelfar" && serverNickname == nickname)
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
        public async Task Secret()
        {
            if (!UserIsNagelf((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You don't have required role to do that");
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsNagelf(SocketGuildUser user)
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


//[Command("Check")]
//public async Task CheckRank()
//{            
//    List<string> nagelfarMembers = GetNagelfarMembers();
//    List<string> discordMembers = GetDiscordMembers();

//    var commonList = nagelfarMembers.Intersect(discordMembers);
//    var differences = discordMembers.Except(nagelfarMembers);

//    var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Nagetest");

//    var users = Context.Guild.Users;
//    var userArray = users.ToArray();

//    await ResetRanks();

//    for (int i = 0; i < users.Count; i++)
//        for(int j = 0; j < commonList.Count(); j++)
//    {

//        var getNickName = userArray[i].Nickname;

//        if (getNickName == null)
//        {
//                if (commonList.ElementAt(j) == userArray[i].Username)
//                {
//                    await userArray[i].AddRoleAsync(role);
//                }
//        }
//        else
//        {
//                if (commonList.ElementAt(j) == userArray[i].Nickname)
//                {

//                    await userArray[i].AddRoleAsync(role);
//                }
//            }
//    }

//     await Context.Channel.SendMessageAsync($"Done");
//}